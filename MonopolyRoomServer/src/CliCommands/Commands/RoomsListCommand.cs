using GameServerParts;
using MonopolyRoomServer.Services;

namespace MonopolyRoomServer.CliCommands
{
    internal class RoomsListCommand : CliCommand
    {
        private RoomService _service;
        private ErrorMessageBuilder _errorMessageBuilder;

        public RoomsListCommand(RoomService service)
        {
            _service = service;
            _errorMessageBuilder = new ErrorMessageBuilder(Header, RequiredArgs);
        }

        private const int RequiredArgs = 0;

        protected override string Header => "roomsList";

        protected override void OnExecute(CommandText text, out string response)
        {
            response = _service.Serialize();
        }

        protected override bool OnValidate(CommandText commandText, out string? errorMessage)
        {
            if(commandText.ArgumentsCount != RequiredArgs)
            {
                errorMessage = _errorMessageBuilder.ArgumentAmountError(commandText.ArgumentsCount);
                return false;
            }
            errorMessage = null;
            return true;
        }
    }
}
