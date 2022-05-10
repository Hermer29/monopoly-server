using GameServerParts.Entities;
using System.Net.Sockets;

namespace MonopolyGameServer.Preparations.SocketInterface
{
    public class GamesRegistering
    {
        private Configurations _configurations;
        private Socket _localServer;
        private IPlayersSentinel _sentinel;

        public GamesRegistering(Configurations configurations, IPlayersSentinel sentinel)
        {
            _configurations = configurations;
            _localServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _sentinel = sentinel;
        }

        public event Action<GameData> GameDataReceived = null!; 

        public void Initialize()
        {
            _localServer.Bind(_configurations.GetGameServerLocalEndPoint());
            _localServer.Listen();

            Console.WriteLine($"Room registering server listening at {_configurations.GetGameServerLocalEndPoint()}");
            AcceptClientsAsync();
        }

        private async void AcceptClientsAsync()
        {
            while (true)
            {
                Socket socket = await _localServer.AcceptAsync();
                Messaging(socket);
            }
        }

        private void Messaging(Socket connected)
        {
            var client = new Client(connected);

            while (true)
            {
                ReceiveClientRequest(client);
            }
        }

        private void ReceiveClientRequest(Client client)
        {
            if (client.TryGetMessage(out string message))
            {
                MessageAcquisitionSuccess(client, message);
            }
        }

        private void MessageAcquisitionSuccess(Client client, string message)
        {
            if (message.StartsWith("isplay"))
            {
                client?.TrySendMessage(IsInGame(message).ToString());
                return;
            }
            GameDataReceived?.Invoke(GameData.Deserialize(message));
        }

        private bool IsInGame(string request)
        {
            return _sentinel.IsPlayerInGame(request.Substring(6));
        }
    }
}
