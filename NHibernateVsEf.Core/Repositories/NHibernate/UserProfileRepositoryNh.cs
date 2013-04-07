using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Context;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.IocAttributes;

namespace NHibernateVsEf.Core.Repositories.NHibernate
{
    [Repository]
    public class UserProfileRepositoryNh : IUserProfileRepositoryNh
    {
        private readonly ISessionFactory _sessionFactory;

        public UserProfileRepositoryNh(ISessionFactory sessionFactory)
        {
            if (sessionFactory == null) throw new ArgumentNullException("sessionFactory");
            _sessionFactory = sessionFactory;
            CurrentSessionContext.Bind(_sessionFactory.OpenSession());
        }

        public UserProfileRepositoryNh():this(new SessionFactoryBuilder().BuildSessionFactory("thread_static")){}

        public void Save(UserProfile profile)
        {
            ISession session = _sessionFactory.GetCurrentSession();
            session.Save(profile);
        }

        public void SyncDb()
        {
            ISession session = _sessionFactory.GetCurrentSession();
            session.Flush();
            session.Clear();
        }

        public UserProfile FindById(string id)
        {
            ISession session = _sessionFactory.GetCurrentSession();
            return session.Load<UserProfile>(id);
        }

        public IList UsersWithoutMusic()
        {
            ISession session = _sessionFactory.GetCurrentSession();
            IList list = session.CreateCriteria<UserTrack>("t")
                .CreateAlias("UserProfile", "u", JoinType.RightOuterJoin)
                .Add(Restrictions.IsNull("t.Id"))
                .List();
            return list;
        }

        public IList<UserProfile> UsersFromNz()
        {
            ISession session = _sessionFactory.GetCurrentSession();
            return session.CreateCriteria<UserProfile>("t")
                   .Add(Restrictions.InsensitiveLike("t.Country", "%ew%zealan%"))
                   .List<UserProfile>();
        }
    }
}