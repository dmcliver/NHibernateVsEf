using System;
using NHibernateVsEf.Core.Repositories.NHibernate;

namespace NHibernateVsEf.Mvc.Tasks
{
    public class UsersWithoutMusicNhQueryTask : IQueryTask
    {
        private readonly IUserProfileRepositoryNh _userProfileRepositoryNh;

        public UsersWithoutMusicNhQueryTask(IUserProfileRepositoryNh userProfileRepositoryNh)
        {
            if (userProfileRepositoryNh == null) throw new ArgumentNullException("userProfileRepositoryNh");
            _userProfileRepositoryNh = userProfileRepositoryNh;
        }

        public void Execute()
        {
            _userProfileRepositoryNh.UsersWithoutMusic();
        }
    }
}