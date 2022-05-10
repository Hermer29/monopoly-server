using GameServerParts;
using GameServerParts.Authentication;
using MonopolyRoomServer.Services;

namespace MonopolyRoomServer.CliCommands
{
    public class CliCommandsFactory
    {
        private RoomService _rooms;
        private AuthenticationService _authenticationService;
        private CliUser _cliUser;
        private CliMessenger _messenger;
        private List<CliCommand> _commands = new List<CliCommand>();

        public CliCommandsFactory(RoomService rooms, CliUser cliUser, AuthenticationService authenticationService, CliMessenger messenger)
        {
            _rooms = rooms;
            _authenticationService = authenticationService;
            _cliUser = cliUser;
            _messenger = messenger;
            CommandsRegistering();
        }

        private void CommandsRegistering()
        {
            _commands.Add(new CreateRoomCommand(_rooms));
            _commands.Add(new RoomsListCommand(_rooms));
            _commands.Add(new ListenCommand(_cliUser, _messenger));
            _commands.Add(new UnlistenCommand(_cliUser));
            _commands.Add(new PlayerCountCommand(_authenticationService));
            _commands.Add(new HelpCommand(_commands.ToArray()));
        }

        public bool TryGetCommand(string text, out CliCommand? command, out string? errorMessage)
        {
            var explodedCommand = CommandText.Parse(text ?? "");
            if(TryFindCommand(explodedCommand, out command) == false)
            {
                errorMessage = $"Command \"{explodedCommand.GetCommand()}\" not found";
                return false;
            }
            if(CheckCommandSyntax(explodedCommand, command, out errorMessage) == false)
            {
                return false;
            }
            return true;
        }

        private bool TryFindCommand(CommandText commandText, out CliCommand sought)
        {
            foreach(CliCommand command in _commands)
            {
                if(command.IsThisCommand(commandText))
                {
                    sought = command;
                    return true;
                }
            }
            sought = new NullCommand();
            return false;
        }

        private bool CheckCommandSyntax(CommandText commandText, CliCommand command, out string? errorMessage)
        {
            return command.Validate(commandText, out errorMessage);
        }
    }
}
