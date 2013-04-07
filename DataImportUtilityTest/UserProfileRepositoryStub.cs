using System.Collections;
using System.Collections.Generic;
using NHibernateVsEf.Core.Domain;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.Repositories.NHibernate;

namespace DataImportUtilityTest
{
    public class UserProfileRepositoryNhStub : IUserProfileRepositoryNh
    {
        public List<UserProfile> UsersPassedIntoSaveMethod { get; private set; }

        public UserProfileRepositoryNhStub()
        {
            UsersPassedIntoSaveMethod = new List<UserProfile>(12);
        }
        
        public void Save(UserProfile profile)
        {
            UsersPassedIntoSaveMethod.Add(profile);
        }

        public void SyncDb()
        {
            
        }

        public UserProfile FindById(string id)
        {
            return _userProfile;
        }

        public IList UsersWithoutMusic()
        {
            return DataForUsersWithoutMusic;
        }

        protected IList DataForUsersWithoutMusic { get; set; }

        public IList<UserProfile> UsersFromNz()
        {
            return DataForUsersFromNz;
        }

        protected IList<UserProfile> DataForUsersFromNz { get; set; }

        private UserProfile _userProfile;

        public void OnFindByIdReturn(UserProfile userProfile)
        {
            _userProfile = userProfile;
        }
    }
}