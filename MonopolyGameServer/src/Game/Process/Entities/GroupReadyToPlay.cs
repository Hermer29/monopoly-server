using GameServerParts.Entities;
using System.Collections;

namespace MonopolyGameServer.Game
{
    public class GroupReadyToPlay : IEnumerable<Process.PlayerInGame>
    {
        private IFormedGroup _source;
        private List<Process.PlayerInGame> _cache = null!;

        public GroupReadyToPlay(IFormedGroup source)
        {
            _source = source;
        }

        public IEnumerable<Process.PlayerInGame> NonDefeatedPlayers => this.Where(player => player.IsDefeated == false);

        private IEnumerable<Player> ShufflePlayers(IEnumerable<Player> players)
        {
            if (players.Count() == 1)
                return players;
            var playersArray = players.ToArray();
            var sequence = new int[playersArray.Length];

            for (int i = 0; i < sequence.Length;)
            {
                var randomInteger = Random.Shared.Next(0, sequence.Length - 1);
                if (sequence.Contains(randomInteger))
                {
                    continue;
                }
                sequence[i] = randomInteger;
            }
            var result = new Player[playersArray.Length];
            
            for (int i = 0; i < playersArray.Length; i++)
            {
                result[i] = playersArray[sequence[i]];
            }

            return result;
        }

        private IEnumerable<Process.PlayerInGame> ConvertIntoInGamePlayers(IEnumerable<Player> players)
        {
            return players.Select(x => new Process.PlayerInGame(x));
        }

        public IEnumerator<Process.PlayerInGame> GetEnumerator()
        {
            if(_cache == null)
            {
                var shuffled = ShufflePlayers(_source);
                _cache = ConvertIntoInGamePlayers(shuffled).ToList();
            }
            return _cache.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
