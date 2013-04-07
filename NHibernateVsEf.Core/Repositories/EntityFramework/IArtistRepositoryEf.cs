using NHibernateVsEf.Core.Domain;

namespace NHibernateVsEf.Core.Repositories.EntityFramework
{
    public interface IArtistRepositoryEf
    {
        ArtistTrackCount GetMostPopularArtist();
    }
}