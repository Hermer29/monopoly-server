using MonopolyGameServer.Game.Process;
using MonopolyGameServer.Game.Properties;

namespace MonopolyGameServer.Game.ProcessPropertiesBorder;

public class FieldService
{
    private readonly FieldController _controller = new FieldController();

    public void HandleStep(TurnData turn, int lastPosition, int newPosition)
    {
        _controller.HandleStep(new PlayerMovesAdapter(turn, lastPosition, newPosition));
    }

    public void BuyCell(int position, TurnData turn)
    {
        _controller.BuyCell(position, new BuyerData
        {
            Id = turn.player.Id,
            TurnData = turn
        });
    }
}