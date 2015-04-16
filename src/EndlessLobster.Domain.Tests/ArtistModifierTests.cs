using Castle.Windsor;
using EndlessLobster.Application;
using EndlessLobster.Domain.Model;
using EndlessLobster.Domain.Repository;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Raven.Client;

namespace EndlessLobster.Domain.Tests
{
    [TestFixture]
    public class ArtistModifierTests
    {
        private IDocumentSession _documentSession;

        [SetUp]
        public void TestFixtureSetUp()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Init(new WindsorContainer());
            _documentSession = bootstrapper.Container.Resolve<IDocumentSession>();
        }

        [TearDown]
        public void TestFixtureTearDown()
        {
            _documentSession.Dispose();
        }

        [Test]
        public void Returns_Modified_ArtistName()
        {
            // arrange
            var artistRepository = new Mock<IRepository<Artist>>();
            var artist = new Artist { Name = "foo" };
            const int artistId = 1;
            artistRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(artist);

            var artistModifier = new ArtistModifier(artistRepository.Object);

            // act
            var actual = artistModifier.ModifyArtistName(artistId, "bar");

            // assert
            actual.Name.Should().Be("foo - bar");
        }

        [Test]
        public void Returns_Modified_ArtistName_Integration()
        {
            // arrange
            var artistRepository = new ArtistRepository(_documentSession);
            var artistModifier = new ArtistModifier(artistRepository);
            const int artistId = 1;

            // act
            var actual = artistModifier.ModifyArtistName(artistId, "foo");

            // assert
            actual.Name.Should().Be("foo - foo");
        }

        [Test]
        [Explicit]
        public void PopulateDatabase()
        {
            var artist = new Artist {ArtistId = 1, Name = "foo"};
            _documentSession.Store(artist);
            _documentSession.SaveChanges();
        }
    }
}