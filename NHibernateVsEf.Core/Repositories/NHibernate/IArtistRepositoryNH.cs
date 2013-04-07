using NHibernateVsEf.Core.Domain;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.Repositories.EntityFramework;

namespace NHibernateVsEf.Core.Repositories.NHibernate
{
    public interface IArtistRepositoryNh
    {
        Artist FindByName(string s);
        void Save(Artist artist);
        void SyncDb();
        ArtistTrackCount GetMostPopularArtist();
    }
}