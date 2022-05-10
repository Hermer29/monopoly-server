using System.Text;

namespace MonopolyGameServer.Game.Process
{
    public static class ClientExtensions
    {
        public static void BroadcastGameRule(this IEnumerable<PlayerInGame> clients, Rule rule, params string[] args)
        {
            var stringBuilder = new StringBuilder($"{(ushort) rule}");
            foreach (var arg in args)
            {
                stringBuilder.Append($" {arg}");
            }
            var message = stringBuilder.ToString();
            clients.BroadcastMessage(message);
        }

        public static void BroadcastGameRule(this IEnumerable<PlayerInGame> clients, Rule rule)
        {
            var message = $"{(ushort) rule}";
            clients.BroadcastMessage(message);
        }

        public static void BroadcastMessage(this IEnumerable<PlayerInGame> players, string message)
        {
            foreach (PlayerInGame player in players)
            {
                player.SendMessage(message);
            }
        }
    }
}
