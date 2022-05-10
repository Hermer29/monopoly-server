using GameServerParts;

namespace MonopolyRoomServer.CliCommands
{
    public class NullCommand : CliCommand
    {
        protected override string Header => "";

        protected override void OnExecute(CommandText text, out string response)
        {
            response = "";
        }

        protected override bool OnValidate(CommandText commandText, out string? errorMessage)
        {
            errorMessage = null;
            return true;
        }
    }
}
