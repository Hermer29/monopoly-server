namespace MonopolyGameServer.Game
{
    public enum Rule : uint
    {
        Null = 0,
        GameStart = 1,
        WinGameEnd = 2,
        PlayerLost = 3,
        PlayerPledges = 4,
        NextTurn = 5,
        PlayerRolls = 6,
        PlayerMoves = 7,
        TieGameEnd = 8,
        TurnStart = 10,
        Pay = 11,
        PlayerCanBuy = 12,
        PlayerPayForInsurance = 13,
        Casino = 14,
        Tax = 15,
        VisitPrison = 16,
        PlayerGoToJail = 17,
        PassByStartCell = 18,
        Rent = 19,
        StepOnPledged = 20,
        PlayerPledged
    }
}
