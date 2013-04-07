using System.Linq;
using NHibernateVsEf.Core.Domain;
using NHibernateVsEf.Core.IocAttributes;

namespace NHibernateVsEf.Core.Repositories.EntityFramework
{
    [Repository]
    public class ArtistRepositoryEf : IArtistRepositoryEf
    {
        private readonly EfContext _context;

        public ArtistRepositoryEf()
        {
            _context = new EfContext(Constants.ConnectionStringName);
        }

        public ArtistTrackCount GetMostPopularArtist() 
        {
            var result = (
                from a in _context.Artists
                join t in _context.Tracks on a equals t.ArtistEf
                group t by a.Name into gpj 
                select new{gpj.Key, Count = gpj.Count()})
                .OrderByDescending(g => g.Count);
            
            var mostPop = result.First();
            
            return new ArtistTrackCount(mostPop.Key, mostPop.Count);
        }
    }
}