namespace MonopolyGameServer.Game.Properties;

public record RailRoadData(
    int BuyCost, 
    int PledgeCost, 
    int[] RentAccordingToBoughtCount
    );