using System.Collections;
using System.Collections.Generic;
using NHibernateVsEf.Core.Domain.EntityFramework;

namespace NHibernateVsEf.Core.Repositories.EntityFramework
{
    public interface IUserProfileRepositoryEf
    {
        IEnumerable<UserTrackEf> UsersWithoutMusic();
        IEnumerable<UserProfileEf> UsersFromNz();
    }
}