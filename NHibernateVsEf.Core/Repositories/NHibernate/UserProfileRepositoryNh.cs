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

        /// <summary>
        /// Saves the user profile to local persistant store
        /// </summary>
        /// <param name="profile"></param>
        public void Save(UserProfile profile)
        {
            ISession session = _sessionFactory.GetCurrentSession();
            session.Save(profile);
        }

        /// <summary>
        /// Syncs the local persistant store with the database
        /// </summary>
        public void SyncDb()
        {
            ISession session = _sessionFactory.GetCurrentSession();
            session.Flush();
            session.Clear();
        }

        /// <summary>
        /// Finds the user by id
        /// </summary>
        public UserProfile FindById(string id)
        {
            ISession session = _sessionFactory.GetCurrentSession();
            return session.Load<UserProfile>(id);
        }

        /// <summary>
        /// Finds all users who do not have a music collection
        /// </summary>
        public IList UsersWithoutMusic()
        {
            ISession session = _sessionFactory.GetCurrentSession();
            IList list = session.CreateCriteria<UserTrack>("t")
                .CreateAlias("UserProfile", "u", JoinType.RightOuterJoin)
                .Add(Restrictions.IsNull("t.Id"))
                .List();
            return list;
        }

        /// <summary>
        /// Finds all users from nz
        /// </summary>
        public IList<UserProfile> UsersFromNz()
        {
            ISession session = _sessionFactory.GetCurrentSession();
            return session.CreateCriteria<UserProfile>("t")
                   .Add(Restrictions.InsensitiveLike("t.Country", "%ew%zealan%"))
                   .List<UserProfile>();
        }
    }
}