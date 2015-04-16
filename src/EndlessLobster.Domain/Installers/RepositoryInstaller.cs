using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EndlessLobster.Domain.Model;
using EndlessLobster.Domain.Repository;
using Raven.Client;
using Raven.Client.Document;

namespace EndlessLobster.Domain.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var documentStore = new DocumentStore {Url = "http://localhost:8080"}.Initialize();
            
            container.Register(
                Component.For<IRepository<Artist>>()
                    .ImplementedBy<ArtistRepository>()
                    .LifestyleTransient(),
                
                Component.For<IDocumentStore>()
                    .Instance(documentStore)
                    .LifestyleSingleton(),

                Component.For<IDocumentSession>()
                    .UsingFactoryMethod(GetDocumentSesssion)
                    .LifestyleTransient()
                );
        }

        static IDocumentSession GetDocumentSesssion(IKernel kernel)
        {
            var store = kernel.Resolve<IDocumentStore>();
            return store.OpenSession();
        }
    }
}