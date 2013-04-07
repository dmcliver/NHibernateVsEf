using System.Collections.Generic;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.Repositories.NHibernate;
using NUnit.Framework;

namespace DataImportUtilityTest
{
    public class TrackRepositoryNhStub : ITrackRepositoryNh
    {
        private Track _track;
        private readonly List<Track> _argsForSave = new List<Track>(12);
        private bool saveWasCalled;

        public Track FindByTitle(string s)
        {
            return _track;
        }

        public void Save(Track track)
        {
            _argsForSave.Add(track);
            saveWasCalled = true;
        }

        public void SyncDb()
        {
        }

        public List<Track> GetArgsForSave()
        {
            return _argsForSave;
        }

        public void VerifySaveWasntCalled()
        {
            Assert.IsFalse(saveWasCalled);
        }

        public void OnFindByTitleReturn(Track track1)
        {
            _track = track1;
        }
    }
}