using GameServerParts.Entities;
using GameServerParts.Exceptions;
using MonopolyRoomServer.CliCommands;
using MonopolyRoomServer.Entities;
using System.Net.Sockets;

namespace MonopolyRoomServer.Services
{
    public class RemoteGameService : IGameService
    {
        private Configurations _configurations;
        private Client _client;
        private CliMessenger _messenger;
        private Socket _socket;
        private bool _isInitialized = false;

        public RemoteGameService(Configurations configurations, CliMessenger messenger)
        {
            _configurations = configurations;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _client = new Client(_socket);
            _messenger = messenger;
        }

        public void CreateGameFromRoom(Room room)
        {
            if (_isInitialized == false)
                throw new InvalidOperationException();

            var info = room.Serialize();
            try
            {
                _client?.TrySendMessage(info);
            }
            catch (ClientDisconnectedException)
            {
                _messenger.SendLine("Remote game service unreachable");
            }
        }

        public void Initialize()
        {
            if(_isInitialized)
                throw new InvalidOperationException();
            
            _isInitialized = true;
            ConnectToGameServer();
        }

        private async void ConnectToGameServer()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    if (TryConnectToGameServer())
                    {
                        OnConnectionSuccessful();
                        return;
                    }
                }
            });
        }

        private void OnConnectionSuccessful()
        {
            Console.WriteLine("Connection to game server successful");
        }

        private bool TryConnectToGameServer()
        {
            try
            {
                _socket.Connect(_configurations.GetGameServerLocalEndPoint());
            }
            catch (SocketException)
            {
                return false;
            }
            return true;
        }

        public bool IsPlaying(string id)
        {
            if (_isInitialized == false)
                throw new InvalidOperationException();

            if(_client.TrySendMessage($"isplay {id}") == false)
            {
                return false;
            }
            if(_client.TryGetMessage(out string result))
            {
                return bool.Parse(result);
            }
            return false;
        }
    }
}
