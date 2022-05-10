using GameServerParts.Exceptions;
using System.Net.Sockets;
using System.Text;

namespace GameServerParts.Entities
{
    public class Client : IDisposable
    {
        private Socket _client;

        public Client(Socket client)
        {
            _client = client;
        }

        public Client(Client client) : this(client._client)
        {
        }

        public Client() : this(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        { 
        }

        public bool IsConnected
        {
            get
            {
                if (_client == null)
                    return false;
                return !((_client.Poll(1000, SelectMode.SelectRead) && (_client.Available == 0)) || !_client.Connected);
            }
        }

        public string ConnectionData => _client.RemoteEndPoint?.ToString() ?? "<Connection termninated>";

        public event Action<Client>? Disconnected;

        public virtual void Dispose()
        {
            if (_client == null)
                return;

            _client?.Dispose();
            _client = null;
            Disconnected?.Invoke(this);
        }

        public virtual async void SendMessageAsync(string text, string endOfMessage = "\n")
        {
            await Task.Run(() => SendMessage(text, endOfMessage));
        }

        public void SendMessage(string text, string endOfMessage = "\n")
        {
            try
            {
                byte[] message = Encoding.UTF8.GetBytes(text + endOfMessage);
                _client?.Send(message, SocketFlags.None);
            }
            catch (SocketException e)
            {
                throw new ClientDisconnectedException(e);
            }
            catch (ObjectDisposedException)
            {
                throw new ClientDisconnectedException();
            }
        }

        public bool TrySendMessage(string text, string endOfMessage = "\n")
        {
            try
            {
                SendMessage(text, endOfMessage);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<string> WaitForMessageAsync(int bufferSize = 255)
        {
            try
            {
                return await Task.Run(() => WaitForMessage(bufferSize));
            }
            catch (ClientDisconnectedException)
            {   
                throw new TaskCanceledException();
            }
        }

        public string WaitForMessage(int bufferSize = 255)
        {
            try
            {
                byte[] message = new byte[bufferSize];
                _client?.Receive(message, SocketFlags.None);
                return Encoding.UTF8.GetString(message).Trim('\0').Trim();
            }
            catch (SocketException e)
            {
                throw new ClientDisconnectedException(e);
            }
            catch (ObjectDisposedException)
            {
                throw new ClientDisconnectedException();
            }
        }
    }
}
