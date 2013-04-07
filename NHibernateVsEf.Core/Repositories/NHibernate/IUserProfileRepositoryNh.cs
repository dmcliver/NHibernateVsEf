using System.Collections;
using System.Collections.Generic;
using NHibernateVsEf.Core.Domain.NHibernate;

namespace NHibernateVsEf.Core.Repositories.NHibernate
{
    public interface IUserProfileRepositoryNh
    {
        void Save(UserProfile profile);
        void SyncDb();
        UserProfile FindById(string id);
        IList UsersWithoutMusic();
        IList<UserProfile> UsersFromNz();
    }
}