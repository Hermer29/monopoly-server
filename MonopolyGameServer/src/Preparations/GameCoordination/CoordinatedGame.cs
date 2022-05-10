using GameServerParts.Entities;
using MonopolyGameServer.Game;
using System.Collections;

namespace MonopolyGameServer.Preparations.Coordination
{
    public class CoordinatedGame : IFormedGroup
    {
        private GameData _correspondingData;
        private List<Player> _players;
        private bool _gameReady;

        public event Action<CoordinatedGame>? GameReady;

        public CoordinatedGame(GameData data)
        {
            _correspondingData = data;
            _players = new List<Player>(_correspondingData.PlayerAmount);
        }

        public void RegisterPlayer(Player player)
        {
            if (_gameReady)
                throw new InvalidOperationException();

            _players.Add(player);

            if (_players.Count == _correspondingData.PlayerAmount)
            {
                _gameReady = true;
                GameReady?.Invoke(this);
            }
        }

        public bool IsWaitingFor(Player player)
        {
            return IsWaitingFor(player.Id);
        }

        public bool IsWaitingFor(string playerId)
        {
            return _correspondingData.PlayersIds.Contains(playerId);
        }

        public IEnumerator<Player> GetEnumerator()
        {
            return _players.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
