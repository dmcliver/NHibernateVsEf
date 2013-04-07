using System.Transactions;
using NHibernateVsEf.Core.Domain;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.Repositories.EntityFramework;
using NHibernateVsEf.Core.Repositories.NHibernate;
using NUnit.Framework;

namespace NHibernateVsEf.Core.Tests.Repositories
{
    [TestFixture]
    public class ArtistRepositoryTest
    {
        [Test]
        public void RepositorySavesDownArtist()
        {
            using (var tx = new TransactionScope())
            {
                var artist1 = new Artist { Name = "adfdf" };
                var artist2 = new Artist {Name = "adfdf"};
                var repo = new ArtistRepositoryNh();
                repo.Save(artist1);
                repo.Save(artist2);
            }
        }

        [Test]
        public void GetMostPopularArtistUsingNHibernate_GetsMostPopularArtist()
        {
            var repo = new ArtistRepositoryNh();
            
            ArtistTrackCount popularArtist = repo.GetMostPopularArtist();
            
            Assert.IsNotNull(popularArtist);
            string artistName = popularArtist.Name;
            Assert.IsFalse(string.IsNullOrEmpty(artistName));
            int count = (int)popularArtist.Count;
            Assert.That(count, Is.GreaterThan(0));
        }

        [Test]
        public void GetMostPopularArtistUsingEntityFramework_GetsMostPopularArtist()
        {
            var repo = new ArtistRepositoryEf();

            ArtistTrackCount popularArtist = repo.GetMostPopularArtist();

            Assert.IsNotNull(popularArtist);
            
            Assert.IsFalse(string.IsNullOrEmpty(popularArtist.Name));
            Assert.That(popularArtist.Count, Is.GreaterThan(0));
        }
    }
}