using GameServerParts;
using GameServerParts.Entities;
using MonopolyRoomServer.Channels;
using MonopolyRoomServer.Entities;
using MonopolyRoomServer.Services;

namespace MonopolyRoomServer.Commands
{
    public class LeftRoomCommand : UserCommand
    {
        private RoomService _service;

        public LeftRoomCommand(RoomService service)
        {
            _service = service;
        }

        protected override void OnTryExecute(CommandText command, Player client, ChannelsAggregator channels)
        {
            if (command.GetCommand() != "left")
                return;

            if (command.ArgumentsCount != 0)
            {
                client?.TrySendMessage("args");
                return;
            }

            if(_service.TryGetRoomByPlayer(out Room room, client))
            {
                room?.Remove(client);
                channels.SwitchChannel(client);
                return;
            }
            client?.TrySendMessage("no room");
        }
    }
}
