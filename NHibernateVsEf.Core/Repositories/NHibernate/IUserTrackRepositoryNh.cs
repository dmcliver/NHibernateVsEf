using NHibernateVsEf.Core.Domain.NHibernate;

namespace NHibernateVsEf.Core.Repositories.NHibernate
{
    public interface IUserTrackRepositoryNh
    {
        void Save(UserTrack userTrack);
        void SyncDb();
    }
}