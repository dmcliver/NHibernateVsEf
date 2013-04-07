using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernateVsEf.Core.Domain.EntityFramework;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.Repositories.EntityFramework;
using NHibernateVsEf.Core.Repositories.NHibernate;
using NUnit.Framework;

namespace NHibernateVsEf.Core.Tests.Repositories
{
    [TestFixture]
    public class UserProfileRepositoryTest
    {
         [Test]
         public void UsersWithMusic_GetsAllUsersWithoutMusic()
         {
             var repo = new UserProfileRepositoryNh();
             IList music = repo.UsersWithoutMusic();
             Assert.That(music.Count, Is.GreaterThan(0));
         }

         [Test]
         public void UsersFromNz_GetsAllUsersFromNz()
         {
             var repo = new UserProfileRepositoryNh();
             IList<UserProfile> users = repo.UsersFromNz();
             Assert.That(users.Count, Is.GreaterThan(0));
             Assert.That(users.All(u => u.Country.ToLower().Equals("new zealand")));
         }

        [Test]
        public void UsersFromNzUsingEf_GetsAllUsersFromNz()
        {
            var repo = new UserProfileRepositoryEf();
            List<UserProfileEf> users = repo.UsersFromNz().ToList();
            Assert.That(users.Count(), Is.GreaterThan(0));
            Assert.That(users.All(u => u.Country.ToLower().Equals("new zealand")));
        }

        [Test]
        public void UsersWithoutMusicUsingEf_GetsAllUsersFromNz()
        {
            var repo = new UserProfileRepositoryEf();
            IEnumerable<UserTrackEf> usersWithoutMusic = repo.UsersWithoutMusic();
            Assert.That(usersWithoutMusic.Count(), Is.GreaterThan(0));
        }

    }
}