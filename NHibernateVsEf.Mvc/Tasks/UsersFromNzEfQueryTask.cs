using System;
using NHibernateVsEf.Core.Repositories.EntityFramework;

namespace NHibernateVsEf.Mvc.Tasks
{
    public class UsersFromNzEfQueryTask : IQueryTask
    {
        private readonly IUserProfileRepositoryEf _userProfileRepositoryEf;

        public UsersFromNzEfQueryTask(IUserProfileRepositoryEf userProfileRepositoryEf)
        {
            if (userProfileRepositoryEf == null) throw new ArgumentNullException("userProfileRepositoryEf");
            _userProfileRepositoryEf = userProfileRepositoryEf;
        }

        public void Execute()
        {
            _userProfileRepositoryEf.UsersFromNz();
        }
    }
}