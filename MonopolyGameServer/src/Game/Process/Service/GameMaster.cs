using MonopolyGameServer.Game.Process.TurnCommands;
using MonopolyGameServer.Game.ProcessPropertiesBorder;

namespace MonopolyGameServer.Game.Process
{
    public class GameMaster
    {
        private List<IFormedGroup> _playersInGames = new List<IFormedGroup>();
        private TurnCommandsFactory _commandsFactory = new TurnCommandsFactory();

        public void Accept(IFormedGroup players)
        {
            _playersInGames.Add(players);

            StartGame(players);
        }

        private void StartGame(IFormedGroup players)
        {
            var inGamePlayers = new GroupReadyToPlay(players);
            new InGameMessageChannel(inGamePlayers);
            var field = new FieldService();
            inGamePlayers.BroadcastGameRule(Rule.GameStart, inGamePlayers.First().Id);
            GameCycle(inGamePlayers, field);
        }

        private async void GameCycle(GroupReadyToPlay group, FieldService field)
        {
            Console.WriteLine("Game started");
            while (true)
            {
                if (group.NonDefeatedPlayers.Count() == 0)
                {
                    group.BroadcastGameRule(Rule.TieGameEnd);
                    break;
                }

                //if (group.NonDefeatedPlayers.Count() == 1)
                //{
                //    players.BroadcastGameRule(Rule.WinGameEnd, players.NonDefeatedPlayers.First().Id);
                //    break;
                //}

                foreach (var player in group.NonDefeatedPlayers)
                {
                    //if (players.Count() < 2)
                    //    break;
                    await MakeTurn(player, group, field);
                    Console.WriteLine("Next turn");
                }
            }
            Console.WriteLine("Game ended");
        }

        private async Task MakeTurn(PlayerInGame player, GroupReadyToPlay group, FieldService field)
        {
            var turn = new PlayerTurn(player, group, _commandsFactory, field);
            await turn.Execute();
        }
    }
}
