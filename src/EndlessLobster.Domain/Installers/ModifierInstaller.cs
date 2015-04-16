using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace EndlessLobster.Domain.Installers
{
    public class ModifierInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IArtistModifier>()
                    .ImplementedBy<ArtistModifier>()
                    .LifestyleTransient()
                );
        }
    }
}