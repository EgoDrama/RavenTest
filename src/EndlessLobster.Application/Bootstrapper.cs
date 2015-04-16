using Castle.Windsor;
using EndlessLobster.Domain.Installers;

namespace EndlessLobster.Application
{
    public class Bootstrapper
    {
        public IWindsorContainer Container { get; private set; }

        public void Init(IWindsorContainer container)
        {
            container.Install(new RepositoryInstaller(), new ModifierInstaller());
            Container = container;
        }
    }
}