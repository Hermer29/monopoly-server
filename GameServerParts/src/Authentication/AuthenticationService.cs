using GameServerParts.Entities;

namespace GameServerParts.Authentication
{
    public abstract class AuthenticationService : IPlayerProvider
    {
        private bool _isInitialized;

        public event Action<Player> PlayerLogged;

        public abstract int LoggedPlayersCount { get; }

        public void Initialize()
        {
            if (_isInitialized)
                throw new InvalidOperationException("Already intialized");

            OnInitialize();
            _isInitialized = true;
        }

        public async void Login(Client client)
        {
            if (_isInitialized == false)
                throw new InvalidOperationException("Initialize first");

            await Task.Run(() => LoginIterations(client));
        }

        private void LoginIterations(Client client)
        {
            while (true)
            {
                if(TryLoginUser(client, out Player? player))
                {
                    OnValidationSuccessful(player ?? throw new NullReferenceException("Player is null after login"));
                    PlayerLogged?.Invoke(player);
                    return;
                }
            }
        }

        protected abstract bool TryLoginUser(Client client, out Player? player);

        protected virtual void OnValidationSuccessful(Player player)
        {

        }

        protected virtual void OnInitialize()
        {

        }
    }
}
