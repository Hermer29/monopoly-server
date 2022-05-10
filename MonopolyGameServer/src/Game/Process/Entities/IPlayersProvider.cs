namespace MonopolyGameServer.Game
{
    public interface IPlayersProvider
    {
        event Action<IFormedGroup> GroupFormed;
    }
}
