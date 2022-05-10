using Autofac;
using GameServerParts.Authentication;
using GameServerParts.Services;
using MonopolyRoomServer.Channels;
using MonopolyRoomServer.CliCommands;
using MonopolyRoomServer.Loggers;
using MonopolyRoomServer.Services;

namespace MonopolyRoomServer.CompositeRoot
{
    public class CompositeRoot
    {
        private ContainerBuilder? _container;

        public CompositeRoot()
        {
            _container = new ContainerBuilder();
        }

        public void Initialize()
        {
            RegisterDependencies();
            RegisterCliService();
            _container.Build();
        }

        private void RegisterCliService()
        {
            _container.RegisterType<CliService>().AsSelf().SingleInstance();
            _container.RegisterType<CliCommandsFactory>().AsSelf().SingleInstance();
            _container.RegisterType<CliUser>().AsSelf().SingleInstance();
            _container.RegisterType<CliMessenger>().AsSelf().SingleInstance();
        }

        private void EarlyRegistration()
        {
            var configurations = new Configurations();
            var connections = new ConnectionService(configurations.GetLobbyServerEndPoint());
            _container.RegisterInstance(connections).AsSelf().SingleInstance();
            _container.RegisterInstance(configurations).AsSelf().SingleInstance();
        }

        private void RegisterDependencies()
        {
            _container.RegisterType<FileLogger>().AsSelf();
            _container.RegisterType<InitializationOrchestration>().AsSelf().SingleInstance().AutoActivate().OnActivated(x => x.Instance.Initialize());

            EarlyRegistration();
            RegisterServices();
            RegisterChannels();
        }

        private void RegisterServices()
        {
            _container.RegisterType<PlayFabCustomIdAuthenticationService>().As<AuthenticationService>().SingleInstance();
            _container.RegisterType<RoomService>().AsSelf().SingleInstance();
            _container.RegisterType<SubscriptionService>().AsSelf().SingleInstance();
            _container.RegisterType<RemoteGameService>().As<IGameService>().SingleInstance().OnActivated(x => x.Instance.Initialize());
        }

        private void RegisterChannels()
        {
            _container.RegisterType<RoomChannel>().AsSelf().SingleInstance();
            _container.RegisterType<LobbyChannel>().AsSelf().SingleInstance();
            _container.RegisterType<ChannelsAggregator>().AsSelf().SingleInstance();
        }
    }
}
