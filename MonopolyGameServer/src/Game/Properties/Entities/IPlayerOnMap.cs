namespace MonopolyGameServer.Game.Properties;

public interface IPlayerOnMap
{
    string Id { get; }
    int LastPosition { get; }
    int NewPosition { get; }
    void AddMoney(int amount, Rule reason);
    void TakeMoney(int amount, Rule reason);
    bool TryTakeMoney(int amount);
    void Say(Rule mapEventType);
    void Say(Rule mapEventType, params string[] args);
    void MovePosition(int position, Rule reason);
}