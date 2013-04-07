using NHibernateVsEf.Core.Domain.NHibernate;

namespace NHibernateVsEf.Core.Repositories.NHibernate
{
    public interface ITrackRepositoryNh
    {
        Track FindByTitle(string s);

        void Save(Track track);
        void SyncDb();
    }
}
