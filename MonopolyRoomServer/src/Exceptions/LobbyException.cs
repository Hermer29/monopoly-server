namespace MonopolyRoomServer.Exceptions
{

    [Serializable]
    public class LobbyException : MonopolyRoomServerException
    {
        public LobbyException() { }
        public LobbyException(string message) : base(message) { }
        public LobbyException(string message, Exception inner) : base(message, inner) { }
        protected LobbyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
