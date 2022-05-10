using GameServerParts.Entities;
using MonopolyRoomServer.Entities;
using MonopolyRoomServer.Services;

namespace MonopolyRoomServer.Channels
{
    public class ChannelsAggregator
    {
        private LobbyChannel _lobby;
        private RoomChannel _room;

        public ChannelsAggregator(RoomService rooms)
        {
            _lobby = new LobbyChannel(rooms, this);
            _room = new RoomChannel(rooms, this);
        }

        public void SwitchChannel(Player player)
        {
            if(_lobby.Contains(player))
            {
                _lobby.Remove(player);
                _room.Accept(player);
                return;
            }
            _room.Remove(player);
            _lobby.Accept(player);
        }

        public void AssignToInitial(Player player)
        {
            _lobby.Accept(player);
        }
    }
}
