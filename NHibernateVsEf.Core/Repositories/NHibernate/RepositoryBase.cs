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

        public void SyncDb()
        {
            ISession session = SessionFactory.GetCurrentSession();
            session.Flush();
            session.Clear();
        }
    }
}