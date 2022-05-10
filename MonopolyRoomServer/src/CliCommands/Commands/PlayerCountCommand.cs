using GameServerParts;
using GameServerParts.Authentication;

namespace MonopolyRoomServer.CliCommands
{
    public class PlayerCountCommand : CliCommand
    {
        private ErrorMessageBuilder _messageBuilder;
        private AuthenticationService _service;

        public PlayerCountCommand(AuthenticationService service)
        {
            _messageBuilder = new ErrorMessageBuilder(Header, ArgsRequired);
            _service = service;
        }

        private const int ArgsRequired = 0;
        protected override string Header => "players";

        protected override void OnExecute(CommandText text, out string response)
        {
            response = $"Logged players count: {_service.LoggedPlayersCount}";
        }

        protected override bool OnValidate(CommandText commandText, out string? errorMessage)
        {
            if(commandText.ArgumentsCount != ArgsRequired)
            {
                errorMessage = _messageBuilder.ArgumentAmountError(commandText.ArgumentsCount);
                return false;
            }
            errorMessage = null;
            return true;
        }
    }
}
