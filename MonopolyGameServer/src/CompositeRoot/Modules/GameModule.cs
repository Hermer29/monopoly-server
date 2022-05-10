using Autofac;
using MonopolyGameServer.Game;
using MonopolyGameServer.Game.Process;

namespace MonopolyGameServer.CompositeRoot.Modules
{
    public class GameModule : Module
    {
        public override void Orchestrate(IContainer container)
        {
            container.Resolve<IPlayersProvider>().GroupFormed += container.Resolve<GameMaster>().Accept;
        }

        public override void Register(ContainerBuilder builder)
        {
            builder.RegisterType<GameMaster>().AsSelf().SingleInstance();
        }
    }
}
