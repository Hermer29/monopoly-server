using System.Runtime.Serialization;

namespace MonopolyGameServer.Game.Properties.Exceptions;

[Serializable]
public class CellNotFoundException : Exception
{
    //
    // For guidelines regarding the creation of new exception types, see
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    // and
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    //

    public CellNotFoundException()
    {
    }

    public CellNotFoundException(string message) : base(message)
    {
    }

    public CellNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }

    protected CellNotFoundException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}    