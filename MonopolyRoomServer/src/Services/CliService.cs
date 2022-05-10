using GameServerParts;
using MonopolyRoomServer.CliCommands;
using static System.Console;

namespace MonopolyRoomServer.Services
{
    public class CliService
    {
        private Configurations _configurations;
        private CliCommandsFactory _factory;
        private CliMessenger _messenger;

        public CliService(Configurations configurations, CliMessenger messenger, CliCommandsFactory factory)
        {
            _configurations = configurations;
            _factory = factory;
            _messenger = messenger;
        }

        public void Start()
        {
            _messenger.SendLine($"Monopoly Room Server {_configurations.GetVersion()}");
            StartTextingCycle();
        }

        private void StartTextingCycle()
        {
            while (true)
            {
                ListenCommand();
            }
        }

        private void ListenCommand()
        {
            _messenger.Send("> ");
            string text = _messenger.Receive();
            string response = ExecuteCommand(text);
            _messenger.SendLine(response);
        }

        public void Send(string text) 
        {
            WriteLine(text);
        }

        private string ExecuteCommand(string text)
        {
            if (_factory.TryGetCommand(text, out var command, out string? errorMessage) == false)
            {
                return errorMessage;
            }
            command.Execute(CommandText.Parse(text), out string response);
            return response;
        }
    }
}
