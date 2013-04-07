using System;
using System.Collections.Generic;
using System.Linq;
using NHibernateVsEf.Core.Domain.EntityFramework;
using NHibernateVsEf.Core.Repositories.EntityFramework;

namespace NHibernateVsEf.Mvc.Tasks
{
    public class UsersWithoutMusicEfQueryTask : IQueryTask
    {
        private readonly IUserProfileRepositoryEf _userProfileRepositoryEf;

        public UsersWithoutMusicEfQueryTask(IUserProfileRepositoryEf userProfileRepositoryEf)
        {
            if (userProfileRepositoryEf == null) throw new ArgumentNullException("userProfileRepositoryEf");
            _userProfileRepositoryEf = userProfileRepositoryEf;
        }

        public void Execute()
        {
            IEnumerable<UserTrackEf> usersWithoutMusic = _userProfileRepositoryEf.UsersWithoutMusic().ToList();
        }
    }
}