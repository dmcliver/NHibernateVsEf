using NHibernate;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.IocAttributes;

namespace NHibernateVsEf.Core.Repositories.NHibernate
{
    [Repository]
    public class UserTrackRepositoryNh : RepositoryBase, IUserTrackRepositoryNh
    {
        public UserTrackRepositoryNh(ISessionFactory sessionFactory) : base(sessionFactory){}

        public UserTrackRepositoryNh():this(new SessionFactoryBuilder().BuildSessionFactory("thread_static")){}

        public void Save(UserTrack userTrack)
        {
            ISession session = SessionFactory.GetCurrentSession();
            session.Save(userTrack);
        }
    }
}