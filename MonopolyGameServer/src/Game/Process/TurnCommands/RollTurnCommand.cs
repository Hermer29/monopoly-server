namespace MonopolyGameServer.Game.Process.TurnCommands
{
    public class RollTurnCommand : TurnCommand
    {
        protected override Rule CorrespondingRule => Rule.PlayerRolls;
        protected override void Executing(TurnData data)
        {
            var lastPosition = data.playerTurn.Position;
            var isRollSucceed = data.playerTurn.TryRollMove(out _);
            var newPosition = data.playerTurn.Position;
            
            if(isRollSucceed)
            {
                data.field.HandleStep(data, lastPosition, newPosition);
            }
        }
    }
}
