using GameServerParts.Entities;
using GameServerParts.Exceptions;

namespace MonopolyRoomServer.Channels
{
    public abstract class Channel
    {
        private Dictionary<Player, CancellationTokenSource> _players = new Dictionary<Player, CancellationTokenSource>();

        public void Accept(Player player)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            _players.Add(player, tokenSource);
            OnEnter(player);
            ReceiveMessagesAsync(player, token);
        }

        public void Remove(Player player)
        {
            _players[player].Cancel();
            _players.Remove(player);
            OnLeave(player);
        }

        public bool Contains(Player player)
        {
            return _players.ContainsKey(player);
        }

        private async void ReceiveMessagesAsync(Player player, CancellationToken token)
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();
                string message = "";
                if (await Task.Run(() => player.TryGetMessage(out message)) == false)
                    return;
                OnReceive(message, player);
            }
        }

        protected abstract void OnReceive(string message, Player player);
        protected abstract void OnEnter(Player player);
        protected abstract void OnLeave(Player player);
    }
}