using System.Runtime.Serialization;

namespace MonopolyGameServer.Game.Properties.Exceptions;

[Serializable]
public class InconsistentUpgradeLevelChangeException : Exception
{
    public InconsistentUpgradeLevelChangeException()
    {
    }

    public InconsistentUpgradeLevelChangeException(string message) : base(message)
    {
    }

    public InconsistentUpgradeLevelChangeException(string message, Exception inner) : base(message, inner)
    {
    }

    protected InconsistentUpgradeLevelChangeException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}