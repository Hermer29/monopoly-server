using GameServerParts;
using GameServerParts.Entities;
using MonopolyRoomServer.Channels;
using MonopolyRoomServer.Entities;
using MonopolyRoomServer.Services;

namespace MonopolyRoomServer.Commands
{
    public class JoinRoomCommand : UserCommand
    {
        private RoomService _service;

        public JoinRoomCommand(RoomService service)
        {
            _service = service;
        }

        protected override void OnTryExecute(CommandText command, Player client, ChannelsAggregator channels)
        {
            if (command.GetCommand() != "join")
                return;

            if(command.ArgumentsCount != 1)
            {
                client?.TrySendMessage("args");
                return;
            }
            if (_service.TryGetRoomById(out Room room, command.GetArguments()[0]))
            {
                channels.SwitchChannel(client);
                if(room.TryAdd(client) == false)
                {
                    client?.TrySendMessage("room overfilled");
                }
                return;
            }
            client?.TrySendMessage("no room");
        }
    }
}
