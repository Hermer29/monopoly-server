using Autofac;
using MonopolyGameServer.CompositeRoot.Modules;

namespace GameMonopolyServer.CompositeRoot
{
    public class CompositeRoot
    {
        private MonopolyGameServer.CompositeRoot.Module[] _modules;

        public CompositeRoot()
        {
            _modules = new MonopolyGameServer.CompositeRoot.Module[]
            {
                new PreparationsModule(),
                new GameModule()
            };
        }

        public void Initialize()
        {
            ContainerBuilder builder = new();
            RegisterModuleServices(builder);
            var container = builder.Build();
            OrchestrateModules(container);
        }

        private void RegisterModuleServices(ContainerBuilder builder)
        {
            foreach(MonopolyGameServer.CompositeRoot.Module module in _modules)
            {
                module.Register(builder);
            }
        }

        private void OrchestrateModules(IContainer container)
        {
            foreach(MonopolyGameServer.CompositeRoot.Module module in _modules)
            {
                module.Orchestrate(container);
            }
        }
    }
}
