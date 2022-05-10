using System.Runtime.Serialization;

namespace MonopolyGameServer.Game.Properties.Exceptions;

[Serializable]
public class CellAlreadyBoughtException : Exception
{
    //
    // For guidelines regarding the creation of new exception types, see
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    // and
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    //

    public CellAlreadyBoughtException()
    {
    }

    public CellAlreadyBoughtException(string message) : base(message)
    {
    }

    public CellAlreadyBoughtException(string message, Exception inner) : base(message, inner)
    {
    }

    protected CellAlreadyBoughtException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}   