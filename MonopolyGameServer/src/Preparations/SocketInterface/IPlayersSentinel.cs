namespace MonopolyGameServer.Preparations.SocketInterface
{
    public interface IPlayersSentinel
    {
        bool IsPlayerInGame(string playerId);
    }
}
