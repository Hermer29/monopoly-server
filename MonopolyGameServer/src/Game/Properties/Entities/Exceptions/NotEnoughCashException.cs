using System.Runtime.Serialization;

namespace MonopolyGameServer.Game.Properties.Exceptions;

[Serializable]
public class NotEnoughCashException : Exception
{
    //
    // For guidelines regarding the creation of new exception types, see
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    // and
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    //

    public NotEnoughCashException()
    {
    }

    public NotEnoughCashException(string message) : base(message)
    {
    }

    public NotEnoughCashException(string message, Exception inner) : base(message, inner)
    {
    }

    protected NotEnoughCashException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}