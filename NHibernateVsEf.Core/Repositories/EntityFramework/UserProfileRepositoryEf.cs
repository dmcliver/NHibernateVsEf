using System.Collections.Generic;
using System.Linq;
using NHibernateVsEf.Core.Domain.EntityFramework;
using NHibernateVsEf.Core.IocAttributes;

namespace NHibernateVsEf.Core.Repositories.EntityFramework
{
    [Repository]
    public class UserProfileRepositoryEf : IUserProfileRepositoryEf
    {
        private readonly EfContext _context;
        public UserProfileRepositoryEf()
        {
            _context = new EfContext(Constants.ConnectionStringName);
        }

        /// <summary>
        /// Finds all users without a music collection
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserTrackEf> UsersWithoutMusic()
        {
            var q = from u in _context.Users
                    join t in _context.UserTracks on u.Id equals t.User.Id
                        into g
                    from res in g.DefaultIfEmpty()
                    where res.User == null
                    select res;

            return q.ToList();
        }

        /// <summary>
        /// Finds all users from Nz
        /// </summary>
        public IEnumerable<UserProfileEf> UsersFromNz()
        {
            IQueryable<UserProfileEf> userProfileEfs = _context.Users.Where(u => u.Country.Contains("ew") && u.Country.Contains("ealan"));
            return userProfileEfs.ToList();
        }
    }
}