using GameServerParts.Authentication;
using GameServerParts.Entities;
using GameServerParts.Services;
using PlayFab;
using PlayFab.ClientModels;
using Rubicks.Extensions;

namespace MonopolyRoomServer.Services
{
    public sealed class PlayFabCustomIdAuthenticationService : AuthenticationService
    {
        private Configurations _configurations;
        private SubscriptionService _subscriptions;
        private IGameService _gameService;
        private Dictionary<Player, IDisposable> _loggedPlayers = new Dictionary<Player, IDisposable>();

        public PlayFabCustomIdAuthenticationService(SubscriptionService subscriptions, Configurations configurations, IGameService gameService)
        {
            _configurations = configurations;
            _subscriptions = subscriptions;
            _gameService = gameService;
        }

        public override sealed int LoggedPlayersCount
        {
            get
            {
                if (_loggedPlayers.Any() == false)
                    return 0;

                return _loggedPlayers.Where(x => x.Key.IsConnected).Count();
            }
        }

        protected override sealed bool TryLoginUser(Client client, out Player? player)
        {
            string token = "";
            if(client.TrySendMessage("token") == false)
            {
                player = null;
                return false;
            }
            if (client?.TryGetMessage(out token) == false)
            {
                player = null;
                return false;
            }

            var loginResult = LoginInPlayFab(token, client);
            if (Validate(loginResult, client, out string? errorMessage))
            {
                player = new Player(client, loginResult.Result.PlayFabId);
                return true;
            }
            player = null;
            return false;
        }

        protected override sealed void OnValidationSuccessful(Player player)
        {
            var subscription = _subscriptions.Subscribe(x => player.TrySendMessage(x.Serialize()));
            _loggedPlayers.Add(player, subscription);
            player.Disconnected += OnPlayerDisconnected;
        }

        protected override sealed void OnInitialize()
        {
            PlayFabSettings.staticSettings.TitleId = _configurations.GetPlayFabTitleId();
            PlayFabSettings.staticSettings.DeveloperSecretKey = _configurations.GetPlayFabDeveloperSecretKey();
            CheckPlayFabSettings();
        }

        private PlayFabResult<LoginResult> LoginInPlayFab(string token, Client client)
        {
            LoginWithCustomIDRequest request = new()
            {
                CustomId = token
            };
            var result = PlayFabClientAPI.LoginWithCustomIDAsync(request);
            Task.WaitAll(result);
            return result.Result;
        }

        private bool Validate(PlayFabResult<LoginResult> result, Client player, out string? errorMessage)
        {
            if (result == null)
            {
                errorMessage = "PlayFab sdk returned null response";
                return false;
            }
            if (result.Error != null)
            {
                errorMessage = $"PlayFab returned error {result.Error.HttpCode}: {result.Error.GenerateErrorReport()}";
                return false;
            }

            string playFabId = result.Result.PlayFabId;
            if (_loggedPlayers.Where(x => x.Key.Id == playFabId && x.Key.IsConnected == true).Any())
            {
                player?.TrySendMessage("already logged");
                errorMessage = "Player already logged";
                return false;
            }
            if (_gameService.IsPlaying(playFabId))
            {
                player?.TrySendMessage("gotogame");
                player?.Dispose();
                errorMessage = "Player must go to game server";
                return false;
            }
            errorMessage = null;
            return true;
        }

        private void CheckPlayFabSettings()
        {
            PlayFabClientAPI.GetTitleDataAsync(new GetTitleDataRequest());
        }

        private void OnPlayerDisconnected(Player player)
        {
            _loggedPlayers[player].Dispose();
            _loggedPlayers.Remove(player);
        }
    }
}
