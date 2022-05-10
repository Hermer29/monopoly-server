using Autofac;

namespace MonopolyGameServer.CompositeRoot
{
    public abstract class Module
    {
        public abstract void Register(ContainerBuilder builder);
        public abstract void Orchestrate(IContainer container);
    }
}
