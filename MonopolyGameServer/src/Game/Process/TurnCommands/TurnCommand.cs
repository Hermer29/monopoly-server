namespace MonopolyGameServer.Game.Process.TurnCommands
{
    public abstract class TurnCommand
    {
        protected abstract Rule CorrespondingRule { get; }

        public void Execute(TurnData data)  
        {
#if DEBUG
            Console.WriteLine($"Reacting on {CorrespondingRule} rule");
#endif
            if (data.response.Rule != CorrespondingRule)
                return;

            Executing(data);
        }

        public bool IsAppliedTo(Rule rule)
        {
            return CorrespondingRule == rule;
        }

        protected abstract void Executing(TurnData data);
    }
}
