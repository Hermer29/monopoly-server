namespace MonopolyGameServer.Game.Properties;

public class TaxCell : SpecialCell
{
    private readonly int _taxSize;
    public TaxCell(int taxSize)
    {
        _taxSize = taxSize;
    }
    public override void EffectOnStep(IPlayerOnMap playerOnMap)
    {
        playerOnMap.TakeMoney(_taxSize, Rule.Tax);
    }
}