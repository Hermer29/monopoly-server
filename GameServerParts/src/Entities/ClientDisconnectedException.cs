using System.Net.Sockets;

namespace GameServerParts.Exceptions
{

    [Serializable]
    public class ClientDisconnectedException : Exception
    {
        public ClientDisconnectedException() : base("Remote player disconnected from room server") { }
        public ClientDisconnectedException(string id) : base(id) { }
        public ClientDisconnectedException(SocketException innerException) : base($"Remote player disconnected from room server. Inner SocketException:\n{innerException.Message}") { }
        public ClientDisconnectedException(string message, Exception inner) : base(message, inner) { }
        protected ClientDisconnectedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
