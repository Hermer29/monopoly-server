using Autofac;
using GameServerParts.Authentication;
using GameServerParts.Services;
using MonopolyGameServer.Game;
using MonopolyGameServer.Preparations.Coordination;
using MonopolyGameServer.Preparations.SocketInterface;

namespace MonopolyGameServer.CompositeRoot.Modules
{
    public class PreparationsModule : Module
    {
        private Configurations _configurations;

        public PreparationsModule()
        { 
            _configurations = new Configurations();
        }

        public override void Register(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configurations).AsSelf().SingleInstance();
            builder.RegisterType<GamesRegistering>().AsSelf().SingleInstance();
            builder.RegisterInstance(new ConnectionService(_configurations.GetGameServerGlobalEndpoint())).AsSelf().SingleInstance();
            builder.RegisterType<PlayersAuthentication>().As<AuthenticationService>().SingleInstance();
            builder.RegisterType<GameCoordinator>().As<IPlayersProvider, IPlayersSentinel, GameCoordinator>().SingleInstance();
        }

        public override void Orchestrate(IContainer container)
        {
            var connectionService = container.Resolve<ConnectionService>();
            var authenticationService = container.Resolve<AuthenticationService>();
            var gameCoordinator = container.Resolve<GameCoordinator>();

            connectionService.UserConnected += authenticationService.Login;
            authenticationService.PlayerLogged += gameCoordinator.Accept;

            authenticationService.Initialize();
            connectionService.Initialize();
            container.Resolve<GamesRegistering>().Initialize();
        }
    }
}
