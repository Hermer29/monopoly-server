using GameServerParts.Entities;
using MonopolyGameServer.Game;
using MonopolyGameServer.Preparations.SocketInterface;

namespace MonopolyGameServer.Preparations.Coordination
{
    public class GameCoordinator : IPlayersSentinel, IPlayersProvider
    {
        private List<CoordinatedGame> _coordinatedGames = new List<CoordinatedGame>();

        public GameCoordinator()
        {
            #region Debug
#if DEBUG
            TEST_FakeData();
#endif
            #endregion
        }

        #region Debug
#if DEBUG
        private void TEST_FakeData()
        {
            var testData = new GameData("N", "123", 1, "1");
            var game = new CoordinatedGame(testData);
            game.GameReady += OnGameReady;
            _coordinatedGames.Add(game);
        }
#endif
        #endregion

        public event Action<IFormedGroup> GroupFormed;

        public void AcceptNewGameData(GameData data)
        {
            var newGame = new CoordinatedGame(data);
            newGame.GameReady += OnGameReady;
            _coordinatedGames.Add(newGame);
        }

        private void OnGameReady(CoordinatedGame game)
        {
            game.GameReady -= OnGameReady;
            _coordinatedGames.Remove(game);
            GroupFormed?.Invoke(game);
        }

        public bool IsPlayerInGame(string playerId)
        {
            return _coordinatedGames.Any(x => x.IsWaitingFor(playerId));
        }

        public void Accept(Player player)
        {
            var gameForPlayer = _coordinatedGames?.First(x => x.IsWaitingFor(player));
            if(gameForPlayer != null)
            {
                gameForPlayer.RegisterPlayer(player);
            }
        }
    }
}
