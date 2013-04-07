using System.Collections.Generic;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.Repositories.NHibernate;
using NUnit.Framework;

namespace DataImportUtilityTest
{
    public class UserTrackRepositoryNhMock : IUserTrackRepositoryNh
    {
        private bool _saveWasCalled;
        private readonly List<UserTrack> _userTracks = new List<UserTrack>(12);

        public void Save(UserTrack userTrack)
        {
            _userTracks.Add(userTrack);
            _saveWasCalled = true;
        }

        public void SyncDb()
        {

        }

        public void VerifySaveWasCalled()
        {
            Assert.IsTrue(_saveWasCalled);
        }

        public List<UserTrack> GetArgsForSave()
        {
            return _userTracks;
        }
    }
}