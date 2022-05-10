namespace MonopolyGameServer.Game.Properties;

public class GoToJailCell : SpecialCell
{
    private int _positionOfPrison;
    
    public GoToJailCell(int positionOfPrison)
    {
        _positionOfPrison = positionOfPrison;
    }
    
    public override void EffectOnStep(IPlayerOnMap playerOnMap)
    {
        playerOnMap.MovePosition(_positionOfPrison, Rule.PlayerGoToJail);
    }
}