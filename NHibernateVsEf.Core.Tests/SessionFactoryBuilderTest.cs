using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernateVsEf.Core.Domain.NHibernate;
using NUnit.Framework;

namespace NHibernateVsEf.Core.Tests
{
    [TestFixture]
    public class SessionFactoryBuilderTest
    {
        [Test]
        public void SessionFactoryBuilder_CreatesDatabase()
        {
            var builder = new SessionFactoryBuilder();
            ISessionFactory factory = builder.BuildSessionFactory("thread_static");
            Assert.NotNull(factory);
        }

        [Test]
        public void SessionFactoryBuilder_GenderMapsToNullableEnum()
        {
            var builder = new SessionFactoryBuilder();
            ISessionFactory factory = builder.BuildSessionFactory("thread_static");
            
            NHibernate.Context.CurrentSessionContext.Bind(factory.OpenSession());
            ISession session = factory.GetCurrentSession();

            IList<UserProfile> userProfiles = session.CreateCriteria<UserProfile>()
                .Add(Restrictions.InsensitiveLike("Country", "%ealan%"))
                .Add(Restrictions.Eq("Gender", Gender.Male))
                .List<UserProfile>();

            Assert.That(userProfiles.Count, Is.GreaterThan(1));
            UserProfile userProfile = userProfiles.First();
            Assert.That(userProfile.Gender, Is.EqualTo(Gender.Male));
        }
    }
}
