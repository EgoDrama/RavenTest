using System.Linq;
using EndlessLobster.Domain.Model;
using Raven.Client;

namespace EndlessLobster.Domain.Repository
{
    public class ArtistRepository : IRepository<Artist>
    {
        private readonly IDocumentSession _documentSession;

        public ArtistRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public Artist Get(int id)
        {
            return _documentSession.Query<Artist>().SingleOrDefault(x => x.ArtistId == id);
        }
    }
}