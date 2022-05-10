namespace MonopolyGameServer.Game.Properties;

public class StartCell : SpecialCell
{
    private readonly int _payAmount;

    public StartCell(int payAmount)
    {
        _payAmount = payAmount;
    }
    
    public override void EffectOnStep(IPlayerOnMap playerOnMap)
    {
        playerOnMap.AddMoney(_payAmount, Rule.PassByStartCell);
    }

    public override void EffectOnPassBy(IPlayerOnMap playerOnMap)
    {
        playerOnMap.AddMoney(_payAmount, Rule.PassByStartCell);
    }
}