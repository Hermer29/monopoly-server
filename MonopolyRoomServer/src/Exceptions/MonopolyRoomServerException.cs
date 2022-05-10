namespace MonopolyRoomServer.Exceptions
{

    [Serializable]
    public class MonopolyRoomServerException : Exception
    {
        public MonopolyRoomServerException() { }
        public MonopolyRoomServerException(string message) : base(message) { }
        public MonopolyRoomServerException(string message, Exception inner) : base(message, inner) { }
        protected MonopolyRoomServerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
