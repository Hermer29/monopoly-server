namespace MonopolyGameServer.Game.Properties;

public record PropertyData(
    int BuyCost, 
    int PledgeCost, 
    int CostOfUpgrade, 
    int RentWithoutSet, 
    int[] RentWithUpgradeLevel 
    );