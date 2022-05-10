namespace MonopolyGameServer.Game.Process
{
    public class InGameMessageChannel
    {
        private GroupReadyToPlay _players;

        public InGameMessageChannel(GroupReadyToPlay players)
        {
            _players = players;

            Subscribe();
        }

        private void Subscribe()
        {
            foreach(var player in _players)
            {
                player.DiceRolled += OnDicesRolled;
                player.Bankrupted += OnPlayerBankrupted;
                player.PositionChanged += OnPlayerMoved;
            }
        }

        private void OnDicesRolled(object? boxedPlayer, (int, int) diceValues)
        {
            Console.WriteLine(diceValues.ToString());
            _players.BroadcastGameRule(Rule.PlayerRolls, diceValues.Item1.ToString(), diceValues.Item2.ToString());
        }

        private void OnPlayerBankrupted(object? boxedPlayer, EventArgs eventArgs)
        {
            var player = (PlayerInGame)boxedPlayer;
            Console.WriteLine("Player bankrupted");
            _players.BroadcastGameRule(Rule.PlayerLost, player.Id);
        }

        private void OnPlayerMoved(object? boxedPlayer, int position)
        {
            var player = (PlayerInGame)boxedPlayer;
            Console.WriteLine($"Player {player.Id} moved to {position} position");
            _players.BroadcastGameRule(Rule.PlayerMoves, player.Id, position.ToString());
        }
    }
}
