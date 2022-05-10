using MonopolyGameServer.Game.Properties.ChanceCellEvents;

namespace MonopolyGameServer.Game.Properties;

public class ChanceCell : SpecialCell
{
    private readonly IEnumerable<IChanceCellEvent> _events;

    public ChanceCell(IEnumerable<IChanceCellEvent> events)
    {
        _events = events;
    }

    public override void EffectOnStep(IPlayerOnMap playerOnMap)
    {
        var randomEvent = _events.Random();
        randomEvent.Execute(playerOnMap);
    }
}