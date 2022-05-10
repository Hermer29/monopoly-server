using GameServerParts.Entities;

namespace MonopolyGameServer.Game.Process
{
    public partial class PlayerInGame
    {
        private Player _player;
        private int _cashAmount = 10000;
        private bool _lost = false;
        private int _position = 0;

        public PlayerInGame(Player player)
        {
            _player = player;
        }

        public event EventHandler? Bankrupted = null!;
        public event EventHandler<int>? CashChanged = null!;
        public event EventHandler<int>? PositionChanged = null!;
        public event EventHandler<(int, int)>? DiceRolled = null!;

        public string Id => _player.Id;
        public bool IsDefeated => _lost;

        public PlayerInGame.TurnHandler GetTurnHandler()
        {
            return new PlayerInGame.TurnHandler(this);
        }

        public void SendMessage(string message)
        {
            _player.SendMessage(message);
        }

        public bool TryGetMessage(out string message)
        {
            return _player.TryGetMessage(out message);
        }

        public async Task<string> ReceiveMessageAsync()
        {
            return await _player.WaitForMessageAsync();
        }
    }
}
