using NHibernateVsEf.Core.Domain;
using NHibernateVsEf.Core.Domain.NHibernate;

namespace NHibernateVsEf.Core.Repositories.NHibernate
{
    public interface IArtistRepositoryNh
    {
        /// <summary>
        /// Finds the artist by name
        /// </summary>
        Artist FindByName(string artistName);

        /// <summary>
        /// Saves the artist down to the local persistant store and syncs it with the database
        /// </summary>
        void Save(Artist artist);
        
        /// <summary>
        ///  syncs the local persistant store with the database
        /// </summary>
        void SyncDb();

        /// <summary>
        /// Gets the most popular artist
        /// </summary>
        ArtistTrackCount GetMostPopularArtist();
    }
}