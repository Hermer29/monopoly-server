using GameServerParts.Entities;
using System.Net;
using System.Net.Sockets;

namespace GameServerParts.Services
{
    public class ConnectionService
    {
        private Socket _server;
        private bool _isInitialized = false;
        private EndPoint _serverEndPoint;

        public event Action<Client>? UserConnected;

        public ConnectionService(EndPoint serverEndPoint)
        {
            _serverEndPoint = serverEndPoint;
            _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Initialize()
        {
            if (_isInitialized == true)
                throw new InvalidOperationException();
            
            _isInitialized = true;
            _server.Bind(_serverEndPoint);
            _server.Listen();
            Console.WriteLine($"Server listening at {_serverEndPoint}");
            ReceiveAsync();
        }

        public async void ReceiveAsync()
        {
            if (_isInitialized == false)
                throw new InvalidOperationException();

            while (true)
            {
                var player = new Client(await _server.AcceptAsync());
                UserConnected?.Invoke(player);
            }
        }
    }
}
