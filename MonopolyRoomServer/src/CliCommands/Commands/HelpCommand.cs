using GameServerParts;

namespace MonopolyRoomServer.CliCommands
{
    public class HelpCommand : CliCommand
    {
        private CliCommand[] _cliCommands;
        private ErrorMessageBuilder _errorBuilder;

        public HelpCommand(params CliCommand[] cliCommands)
        {
            _cliCommands = cliCommands;
            _errorBuilder = new ErrorMessageBuilder(Header, 0);
        }

        protected override string Header => "help";
        private const int RequiredArgs = 0;

        protected override void OnExecute(CommandText text, out string response)
        {
            var cliCommandsNamesText = _cliCommands.Select(x => x.CommandHeader).Aggregate((a, x) => a += " " + x + " ").Trim(' ').Trim(',');
            response = cliCommandsNamesText;
        }

        protected override bool OnValidate(CommandText commandText, out string? errorMessage)
        {
            if(commandText.ArgumentsCount != RequiredArgs)
            {
                errorMessage = _errorBuilder.ArgumentAmountError(commandText.ArgumentsCount);
                return false;
            }
            errorMessage = null;
            return true;
        }
    }
}
