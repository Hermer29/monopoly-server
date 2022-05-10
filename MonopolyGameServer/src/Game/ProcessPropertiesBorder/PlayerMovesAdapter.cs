using MonopolyGameServer.Game.Process;
using MonopolyGameServer.Game.Properties;

namespace MonopolyGameServer.Game.ProcessPropertiesBorder;

public class PlayerMovesAdapter : IPlayerOnMap
{
    private TurnData _turn;
    private int _lastPosition;
    private int _newPosition;
    
    public PlayerMovesAdapter(TurnData turn, int lastPosition, int newPosition)
    {
        _turn = turn;
        _lastPosition = lastPosition;
        _newPosition = newPosition;
    }

    public string Id => _turn.player.Id;
    public int LastPosition => _lastPosition;
    public int NewPosition => _newPosition;
    public void AddMoney(int amount, Rule reason)
    {
        _turn.playerTurn.AddCash(amount);
    }

    public void TakeMoney(int amount, Rule reason)
    {
        _turn.playerTurn.RemoveCashForce(amount);
    }

    public bool TryTakeMoney(int amount)
    {
        if (!_turn.playerTurn.IsHasEnoughMoney(amount)) 
            return false;
        _turn.playerTurn.RemoveCashForce(amount);
        return true;
    }

    public void Say(Rule reason)
    {
        _turn.playersInGroup.BroadcastGameRule(reason);
    }

    public void Say(Rule reason, params string[] args)
    {
        _turn.playersInGroup.BroadcastGameRule(reason, args);
    }

    public void MovePosition(int position, Rule reason)
    {
        _turn.playerTurn.MovePlayerForce(position);
        _turn.playersInGroup.BroadcastGameRule(reason);
    }
}