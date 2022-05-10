using System.Runtime.Serialization;

namespace MonopolyGameServer.Game.Properties.Exceptions;

[Serializable]
public class UpgradeLimitException : Exception
{

    public UpgradeLimitException()
    {
    }

    public UpgradeLimitException(string message) : base(message)
    {
    }

    public UpgradeLimitException(string message, Exception inner) : base(message, inner)
    {
    }

    protected UpgradeLimitException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}