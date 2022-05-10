using GameServerParts;

namespace MonopolyGameServer.Game
{
    public static class RuleExtensions
    {
        public static Rule ParseRule(this string message)
        {
            return (Rule) Enum.Parse(typeof(Rule), message);
        }
    }
}
