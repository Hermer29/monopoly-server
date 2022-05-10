namespace MonopolyRoomServer.Exceptions
{

    [Serializable]
    public class ServiceException : MonopolyRoomServerException
    {
        public ServiceException(string message) : base(message) { }
        public ServiceException() : base("Something went wrong with service") { }
        public ServiceException(string message, Exception inner) : base(message, inner) { }
        protected ServiceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
