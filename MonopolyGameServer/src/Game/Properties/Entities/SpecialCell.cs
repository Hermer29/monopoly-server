namespace MonopolyGameServer.Game.Properties;

public abstract class SpecialCell
{
    public abstract void EffectOnStep(IPlayerOnMap playerOnMap);
    public virtual void EffectOnPassBy(IPlayerOnMap playerOnMap) {}
}