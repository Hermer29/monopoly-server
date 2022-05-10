using GameServerParts.Entities;
using MonopolyRoomServer.Exceptions;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace MonopolyRoomServer.Entities
{

    public class Room : IEnumerable<PlayerInRoom>, ISerializable
    {
        private List<PlayerInRoom> _players;

        public Room(string name, int capacity, params Player[] players)
        {
            Capacity = capacity;
            Name = name;
            _players = new List<PlayerInRoom>(players.Select(player => new PlayerInRoom(player)));
            Id = Guid.NewGuid().ToString();
        }

        public Room(string name, int capacity) : this(name, capacity, new Player[] {}) { }

        public string Name { get; }
        public string Id { get; }
        public int Capacity { get; }
        public bool IsOverfilled => PlayerCount == Capacity;
        public int PlayerCount => _players.Count;

        public event Action<Room>? Updated;
        public event Action<Room>? FilledUp;
        public event Action<Room>? Empty;
        
        public void Add([DisallowNull] Player player)
        {
            if (IsOverfilled)
                throw new RoomException();
            
            _players.Add(new PlayerInRoom(player));
            Updated?.Invoke(this);

            if(IsOverfilled)
                FilledUp?.Invoke(this);
        }

        public bool TryAdd([DisallowNull] Player player)
        {
            try
            {
                Add(player);
            }
            catch (RoomException)
            {
                return false;
            }
            return true;
        }

        public void Remove(Player player)
        {
            _players.Remove(_players.Where(x => x.Player == player).First());
            Updated?.Invoke(this);
        }

        public bool Contains(Player player)
        {
            return _players.Any(x => x.Player == player);
        }

        public IEnumerator<PlayerInRoom> GetEnumerator()
        {
            return _players.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string Serialize()
        {
            string message = $"{Name};{Id};{PlayerCount};{Capacity};";
            foreach (var player in _players)
            {
                message += $"{player.Serialize()};";
            }
            return message;
        }
    }
}
