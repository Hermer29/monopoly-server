using GameServerParts;

namespace MonopolyRoomServer.CliCommands
{
    public abstract class CliCommand
    {
        protected abstract string Header { get; }

        public void Execute(CommandText text, out string response)
        {
            if(Validate(text, out string? errorMessage) == false)
            {
                response = errorMessage;
                throw new InvalidOperationException(errorMessage);
            }
            OnExecute(text, out response);
        }

        public string CommandHeader => Header;

        protected abstract void OnExecute(CommandText text, out string response);
        
        public bool IsThisCommand(CommandText text)
        {
            return text.GetCommand() == Header;
        }

        public bool Validate(CommandText commandText, out string? errorMessage)
        {
            if (IsThisCommand(commandText) == false)
            {
                errorMessage = "Passed command text is not this command";
                return false;
            }
            return OnValidate(commandText, out errorMessage);
        }

        protected abstract bool OnValidate(CommandText commandText, out string? errorMessage);
    }
}
