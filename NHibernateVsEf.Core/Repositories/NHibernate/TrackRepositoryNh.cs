using NHibernate;
using NHibernate.Criterion;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.IocAttributes;

namespace NHibernateVsEf.Core.Repositories.NHibernate
{
    [Repository]
    public class TrackRepositoryNh : RepositoryBase, ITrackRepositoryNh
    {
        public TrackRepositoryNh(ISessionFactory sessionFactory) : base(sessionFactory){}

        public TrackRepositoryNh():this(new SessionFactoryBuilder().BuildSessionFactory("thread_static")){}

        public Track FindByTitle(string s)
        {
            ISession session = SessionFactory.GetCurrentSession();
            return session.CreateCriteria<Track>()
                .Add(Restrictions.Eq("Title", s))
                .SetMaxResults(1)
                .UniqueResult<Track>();
        }

        public void Save(Track track)
        {
            ISession session = SessionFactory.GetCurrentSession();
            session.Save(track);
            session.Flush();
        }
    }
}
