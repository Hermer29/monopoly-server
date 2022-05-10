namespace MonopolyGameServer.Game.Properties;

public class MedicalInsuranceEvent : IChanceCellEvent
{
    public Rule Message => Rule.PlayerPayForInsurance;

    public void Execute(IPlayerOnMap player)
    {
        player.TakeMoney(200, Rule.PlayerPayForInsurance);
    }
}