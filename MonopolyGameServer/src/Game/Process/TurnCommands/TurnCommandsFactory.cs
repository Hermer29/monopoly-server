namespace MonopolyGameServer.Game.Process.TurnCommands
{
    public class TurnCommandsFactory
    {
        private TurnCommand[] _playerTurns = new TurnCommand[]
        {
            new RollTurnCommand()
        };

        public TurnCommand GetCommand(Rule rule)
        {
            foreach (var turn in _playerTurns)
            {
                if (turn.IsAppliedTo(rule))
                {
                    return turn;
                }
            }
            throw new InvalidOperationException("Applied action not found in gameActions array");
        }
    }
}
