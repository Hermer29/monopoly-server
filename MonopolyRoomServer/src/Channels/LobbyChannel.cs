using GameServerParts;
using GameServerParts.Entities;
using MonopolyRoomServer.Commands;
using MonopolyRoomServer.Services;

namespace MonopolyRoomServer.Channels
{
    public class LobbyChannel : Channel
    {
        private RoomService _rooms;
        private UserCommand[] _commands;
        private ChannelsAggregator _channels;

        public LobbyChannel(RoomService rooms, ChannelsAggregator channels)
        {
            _rooms = rooms;
            _commands = new UserCommand[]
            {
                new CreateRoomCommand(rooms),
                new JoinRoomCommand(rooms)
            };
            _channels = channels;
        }

        protected override void OnEnter(Player player)
        {
            player.TrySendMessage("in lobby");
            player.TrySendMessage(_rooms.Serialize());
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
            foreach(var command in _commands)
            {
                command.TryExecute(commandText, player, _channels);
            }
        }
    }
}
