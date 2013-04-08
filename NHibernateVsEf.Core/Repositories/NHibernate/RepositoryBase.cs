using System;
using NHibernate;
using NHibernate.Context;

namespace NHibernateVsEf.Core.Repositories.NHibernate
{
    public abstract class RepositoryBase
    {
        protected readonly ISessionFactory SessionFactory;

        public RepositoryBase(ISessionFactory sessionFactory)
        {
            if (sessionFactory == null) throw new ArgumentNullException("sessionFactory");
            SessionFactory = sessionFactory;
            CurrentSessionContext.Bind(sessionFactory.OpenSession());
        }

        /// <summary>
        /// Syncs the local persistant store with the database 
        /// </summary>
        public void SyncDb()
        {
            ISession session = SessionFactory.GetCurrentSession();
            session.Flush();
            session.Clear();
        }
    }
}