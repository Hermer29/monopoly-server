using GameServerParts;

namespace MonopolyRoomServer.CliCommands
{
    internal class UnlistenCommand : CliCommand
    {
        private CliUser _cli;
        private ErrorMessageBuilder _errorResponseBuilder;

        public UnlistenCommand(CliUser cli)
        {
            _cli = cli;
            _errorResponseBuilder = new ErrorMessageBuilder(Header, RequiredArgs);
        }

        private const int RequiredArgs = 0;
        protected override string Header => "unlisten";

        protected override void OnExecute(CommandText text, out string response)
        {
            _cli.UnsubscribeToRoomChanges();
            response = "Unlistened";
        }

        protected override bool OnValidate(CommandText commandText, out string? errorMessage)
        {
            errorMessage = null;

            if (commandText.ArgumentsCount != RequiredArgs)
            {
                errorMessage = _errorResponseBuilder.ArgumentAmountError(commandText.ArgumentsCount);
                return false;
            }
            if(_cli.IsSubscribed == false)
            {
                errorMessage = _errorResponseBuilder.WithHeader("Subscribe first");
                return false;
            }
            return true;
        }
    }
}
