using GameServerParts.Entities;
using MonopolyRoomServer.Entities;
using MonopolyRoomServer.Exceptions;
using System.Text;

namespace MonopolyRoomServer.Services
{
    public class RoomService : ISerializable
    {
        private List<Room> _rooms = new List<Room>();

        public event Action<Room>? RoomAdded;
        public event Action<Room>? RoomRemoved;
        public event Action<Room>? RoomUpdated;
        public event Action<Room>? RoomOverfilled;

        public void Add(Room room)
        {
            lock (new object())
            {
                if (_rooms.Contains(room))
                {
                    throw new RoomException("Room already exists");
                }
                if(room.IsOverfilled)
                {
                    OnRoomOverfilled(room);
                    return;
                }
                _rooms.Add(room);
                RoomAdded?.Invoke(room);
                room.Updated += OnRoomUpdated;
                room.FilledUp += OnRoomOverfilled;
            }
        }

        private void OnRoomOverfilled(Room room)
        {
            RoomOverfilled?.Invoke(room);
            foreach(var player in room)
            {
                player.SayGoToGame();
            }
            room.Updated -= OnRoomUpdated;
            room.FilledUp -= OnRoomOverfilled;
            room.Updated -= OnRoomUpdated;
            TryRemove(room);
        }

        private void OnRoomUpdated(Room room)
        {
            RoomUpdated?.Invoke(room);
            if (room.PlayerCount == 0)
            {
                Remove(room);
            }
        }

        private void TryRemove(Room room)
        {
            try
            {
                Remove(room);
            }
            catch (RoomException)
            {
                return;
            }
        }

        private void Remove(Room room)
        {
            if (_rooms.Contains(room) == false)
            {
                throw new RoomException("Room to remove doesnt exists");
            }
            _rooms.Remove(room);
            RoomRemoved?.Invoke(room);
            room.Updated -= OnRoomUpdated;
        }

        public bool TryGetRoomByPlayer(out Room room, Player player)
        {
            return TryGetRoom(out room, x => x.Contains(player));
        }

        public bool TryGetRoomById(out Room room, string id)
        {
            return TryGetRoom(out room, x => x.Id == id);
        }

        private bool TryGetRoom(out Room room, Func<Room, bool> predicate)
        {
            try
            {
                room = WhereRoom(predicate).First();
            }
            catch (RoomException)
            {
                room = null;
                return false;
            }
            return true;
        }

        private IEnumerable<Room> WhereRoom(Func<Room, bool> predicate)
        {
            if (_rooms.Count == 0)
            {
                throw new RoomException("No rooms found");
            }
            var room = _rooms.Where(predicate);
            if (room.Any() == false)
            {
                throw new RoomException("No rooms found");
            }
            return room;
        }

        public string Serialize()
        {
            if(_rooms.Count == 0)
            {
                return "no rooms";
            }
            var message = new StringBuilder();
            foreach(var room in _rooms)
            {
                message.Append(room.Serialize());
            }
            return message.ToString();
        }
    }
}
