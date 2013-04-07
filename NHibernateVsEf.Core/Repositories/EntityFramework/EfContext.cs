using System.Data.Entity;
using NHibernateVsEf.Core.Domain.EntityFramework;

namespace NHibernateVsEf.Core.Repositories.EntityFramework
{
    public class EfContext : DbContext
    {
        public EfContext(string db):base(db)
        {
            Database.SetInitializer<EfContext>(null);
        }

        public DbSet<ArtistEf> Artists { get; set; } 
        public DbSet<TrackEf> Tracks { get; set; } 
        public DbSet<UserProfileEf> Users { get; set; } 
        public DbSet<UserTrackEf> UserTracks { get; set; } 
    }
}