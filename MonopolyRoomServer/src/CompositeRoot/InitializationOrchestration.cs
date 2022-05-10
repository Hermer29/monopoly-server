using GameServerParts.Authentication;
using GameServerParts.Services;
using MonopolyRoomServer.Channels;
using MonopolyRoomServer.Services;

namespace MonopolyRoomServer.CompositeRoot
{
    public class InitializationOrchestration
    {
        private AuthenticationService _authentication;
        private ConnectionService _connections;
        private IGameService _gameService;
        private RoomService _roomService;
        private ChannelsAggregator _channels;
        private CliService _cli;

        public InitializationOrchestration(CliService cli, ConnectionService connections, AuthenticationService authentication, ChannelsAggregator channels, IGameService gameService, RoomService roomService)
        {
            _authentication = authentication;
            _channels = channels;
            _cli = cli;
            _connections = connections;
            _gameService = gameService;
            _roomService = roomService;

            Register();
        }

        private void Register()
        {
            _connections.UserConnected += _authentication.Login;
            _authentication.PlayerLogged += _channels.AssignToInitial;
            _roomService.RoomOverfilled += _gameService.CreateGameFromRoom;
        }

        public void Initialize()
        {
            _authentication.Initialize();
            _connections.Initialize();
            _cli.Start();
            _gameService.Initialize();
        }
    }
}
