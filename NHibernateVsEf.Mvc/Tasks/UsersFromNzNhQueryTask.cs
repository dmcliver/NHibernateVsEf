using System;
using NHibernateVsEf.Core.Repositories.NHibernate;

namespace NHibernateVsEf.Mvc.Tasks
{
    public class UsersFromNzNhQueryTask : IQueryTask
    {
        private readonly IUserProfileRepositoryNh _userProfileRepositoryNh;

        public UsersFromNzNhQueryTask(IUserProfileRepositoryNh userProfileRepositoryNh)
        {
            if (userProfileRepositoryNh == null) throw new ArgumentNullException("userProfileRepositoryNh");
            _userProfileRepositoryNh = userProfileRepositoryNh;
        }

        public void Execute()
        {
            _userProfileRepositoryNh.UsersFromNz();
        }
    }
}