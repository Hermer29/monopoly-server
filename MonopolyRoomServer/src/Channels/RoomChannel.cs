using GameServerParts;
using GameServerParts.Entities;
using MonopolyRoomServer.Commands;
using MonopolyRoomServer.Entities;
using MonopolyRoomServer.Services;

namespace MonopolyRoomServer.Channels
{
    public class RoomChannel : Channel
    {
        private ChannelsAggregator _channels;
        private RoomService _rooms;
        private UserCommand[] _commands;

        public RoomChannel(RoomService rooms, ChannelsAggregator channels)
        {
            _channels = channels;
            _rooms = rooms;
            _commands = new UserCommand[] {
                new LeftRoomCommand(rooms)
            };
        }

        protected override void OnEnter(Player player)
        {
            if(_rooms.TryGetRoomByPlayer(out Room room, player))
            {
                GreetClient(player);
                player?.TrySendMessage(room.Serialize());
                return;
            }
            player.Dispose();
        }

        private void GreetClient(Player player)
        {
            player?.TrySendMessage("in room");
        }

        protected override void OnLeave(Player player)
        {
        }

        protected override void OnReceive(string message, Player player)
        {
            ParseMessage(message, player);
        }

        private void ParseMessage(string message, Player player)
        {
            var commandText = CommandText.Parse(message);
            foreach (var command in _commands)
            {
                command.TryExecute(commandText, player, _channels);
            }
        }
    }
}
