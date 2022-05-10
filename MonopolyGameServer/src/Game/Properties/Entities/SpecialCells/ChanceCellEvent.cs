namespace MonopolyGameServer.Game.Properties;

public interface IChanceCellEvent
{
    Rule Message{ get; }
    void Execute(IPlayerOnMap player);
}