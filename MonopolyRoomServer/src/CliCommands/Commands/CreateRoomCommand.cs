using GameServerParts;
using MonopolyRoomServer.Entities;
using MonopolyRoomServer.Services;

namespace MonopolyRoomServer.CliCommands
{
    public class CreateRoomCommand : CliCommand
    {
        private RoomService _service;
        private ErrorMessageBuilder _errorResponseBuilder;

        public CreateRoomCommand(RoomService service)
        {
            _service = service;
            _errorResponseBuilder = new ErrorMessageBuilder(Header, RequiredArgs);
        }

        private const int RequiredArgs = 2;
        protected override sealed string Header => "createRoom";

        protected override void OnExecute(CommandText text, out string response)
        {
            var args = text.GetArguments();
            var roomName = args[0];
            var roomCapacity = int.Parse(args[1]);
            var room = new Room(roomName, roomCapacity);
            _service.Add(room);
            response = $"Created room \"{args[0]}\", with ID: {room.Id}";
        }

        protected override bool OnValidate(CommandText commandText, out string? errorMessage)
        {
            errorMessage = null;
            
            if(commandText.ArgumentsCount != RequiredArgs)
            {
                errorMessage = _errorResponseBuilder.ArgumentAmountError(commandText.ArgumentsCount);
                return false;
            }
            return true;
        }
    }
}
