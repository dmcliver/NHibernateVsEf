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

        /// <summary>
        /// Saves the track to the local persistant store
        /// </summary>
        /// <param name="userTrack"></param>
        public void Save(UserTrack userTrack)
        {
            ISession session = SessionFactory.GetCurrentSession();
            session.Save(userTrack);
        }
    }
}