using MonopolyGameServer.Game.Process.TurnCommands;
using MonopolyGameServer.Game.ProcessPropertiesBorder;

namespace MonopolyGameServer.Game.Process
{
    public class PlayerTurn
    {
        private PlayerInGame _player;
        private GroupReadyToPlay _group;
        private TurnCommandsFactory _commandsFactory;
        private FieldService _field;

        public PlayerTurn(PlayerInGame player, GroupReadyToPlay group, TurnCommandsFactory commandsFactory, FieldService field)
        {
            _player = player;
            _group = group;
            _commandsFactory = commandsFactory;
            _field = field;
        }

        private TimeSpan WaitTime { get; } = TimeSpan.FromSeconds(999);

        public async Task Execute()
        {
            var turnHandler = _player.GetTurnHandler();
            await StartTurn(_player, turnHandler, _group);
        }

        private async Task StartTurn(PlayerInGame player, PlayerInGame.TurnHandler playerTurn, GroupReadyToPlay players)
        {
            Response? response = null;
            do
            {
                Console.WriteLine(playerTurn.HasActions);
                if (await Task.Run(() => TryReceiveMessageWithTimeout(player, out response)))
                {
                    ExecuteCommand(response.Value, players, playerTurn, _field, player);
                    continue;
                }
                playerTurn.TimeOutLost();
                break;
            }
            while (playerTurn.HasActions);
        }

        private bool TryReceiveMessageWithTimeout(PlayerInGame player, out Response? response)
        {
            var messageReceiveTask = player.ReceiveMessageAsync();
            var isInTime = messageReceiveTask.Wait(WaitTime);
            if (isInTime)
            {
                response = new Response(messageReceiveTask.Result);
                return true;
            }
            response = null;
            return false;
        }

        private void ExecuteCommand(Response response, GroupReadyToPlay players, PlayerInGame.TurnHandler playerTurn, 
            FieldService field, PlayerInGame player)
        {
            try
            {
                var command = _commandsFactory.GetCommand(response.Rule);
                command.Execute(new TurnData(response, playerTurn, players, field, player));
            }
            catch (InvalidOperationException e)
            {
                players.BroadcastGameRule(Rule.Null, "Unexpected command");
            }
        }
    }
}
