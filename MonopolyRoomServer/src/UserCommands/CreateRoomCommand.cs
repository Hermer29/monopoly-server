using GameServerParts;
using GameServerParts.Entities;
using MonopolyRoomServer.Channels;
using MonopolyRoomServer.Entities;
using MonopolyRoomServer.Services;

namespace MonopolyRoomServer.Commands
{
    public class CreateRoomCommand : UserCommand
    {
        private RoomService _rooms;

        public CreateRoomCommand(RoomService rooms)
        {
            _rooms = rooms;
        }

        protected override void OnTryExecute(CommandText command, Player client, ChannelsAggregator channels)
        {
            if (command.GetCommand() != "create")
                return;

            if(command.ArgumentsCount != 2)
            {
                client?.TrySendMessage("args");
                return;
            }

            string name = command.GetArguments()[0];
            int playersCapacity = int.Parse(command.GetArguments()[1]);
            var room = new Room(name, playersCapacity, client);
            _rooms.Add(room);
            channels.SwitchChannel(client);
        }
    }
}
