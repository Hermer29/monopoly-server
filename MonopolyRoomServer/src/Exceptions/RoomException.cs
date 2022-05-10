namespace MonopolyRoomServer.Exceptions
{

    [Serializable]
    public class RoomException : MonopolyRoomServerException
    {
        public RoomException() : base() { }
        public RoomException(string message) : base(message) { }
        public RoomException(string message, Exception inner) : base(message, inner) { }
        protected RoomException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
