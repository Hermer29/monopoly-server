using GameServerParts.Authentication;
using GameServerParts.Entities;

namespace MonopolyGameServer.Preparations.SocketInterface
{
    public class PlayersAuthentication : AuthenticationService
    {
        private List<Player> _players = new List<Player>();
        private IPlayersSentinel _games;

        public PlayersAuthentication(IPlayersSentinel games)
        {
            _games = games;
        }

        public override int LoggedPlayersCount => _players.Where(x => x.IsConnected).Count();

        protected override void OnValidationSuccessful(Player player)
        {
            _players.Add(player);
            player.Disconnected += OnPlayerDisconnected;
        }

        protected override bool TryLoginUser(Client client, out Player? player)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            player = null;
            if (client.TrySendMessage("id") == false)
            {
                return false;
            }
            bool isLoginSucceeded = client.TryGetMessage(out string id) && _games.IsPlayerInGame(id);

            if (isLoginSucceeded)
            {
                player = new Player(client, id);
            }
            return isLoginSucceeded;
        }

        private void OnPlayerDisconnected(Player player)
        {
            _players.Remove(player);
            player.Disconnected -= OnPlayerDisconnected;
        }
    }
}
