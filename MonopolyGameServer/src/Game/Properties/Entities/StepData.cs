namespace MonopolyGameServer.Game.Properties;

public record StepData(
    int Position,
    bool CanBuy,
    int BuyCost,
    bool Owned,
    string OwnerId,
    int Rent,
    bool IsSpecial,
    bool Pledged,
    Action<IPlayerOnMap>? ActionOnStep,
    Action<IPlayerOnMap>? ActionOnPassBy);