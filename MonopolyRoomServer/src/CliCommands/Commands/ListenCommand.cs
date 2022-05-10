using GameServerParts;
using MonopolyRoomServer.Services;

namespace MonopolyRoomServer.CliCommands
{
    public class ListenCommand : CliCommand
    {

        private ErrorMessageBuilder _errorMessageBuilder;
        private CliUser _cli;
        private CliMessenger _messenger;

        public ListenCommand(CliUser cli, CliMessenger messenger)
        {
            _errorMessageBuilder = new ErrorMessageBuilder(Header, RequiredArgs);
            _cli = cli;
            _messenger = messenger;
        }

        protected override string Header => "listen";
        private const int RequiredArgs = 0;

        protected override void OnExecute(CommandText text, out string response)
        {
            _cli.SubscribeToRoomChanges(x => _messenger.SendLine(x));
            response = "Start listening room events";
        }

        protected override bool OnValidate(CommandText commandText, out string? errorMessage)
        {
            if (commandText.ArgumentsCount != RequiredArgs)
            {
                errorMessage = _errorMessageBuilder.ArgumentAmountError(commandText.ArgumentsCount);
                return false;
            }

            if(_cli.IsSubscribed)
            {
                errorMessage = _errorMessageBuilder.WithHeader("Already subscribed");
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
