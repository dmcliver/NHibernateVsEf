using System.Collections.Generic;
using NHibernateVsEf.Core.Domain;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.Repositories;
using NHibernateVsEf.Core.Repositories.EntityFramework;
using NHibernateVsEf.Core.Repositories.NHibernate;
using NUnit.Framework;

namespace DataImportUtilityTest
{
    public class ArtistRepositoryNhStub : IArtistRepositoryNh
    {
        private Artist _artist;
        private readonly List<Artist> _argsForSave = new List<Artist>(12);
        private bool _saveWasCalled;

        public void OnFindByNameReturn(Artist artist)
        {
            _artist = artist;
        }

        public Artist FindByName(string s)
        {
            return _artist;
        }

        public void Save(Artist artist)
        {
            _saveWasCalled = true;
            _argsForSave.Add(artist);
        }

        public void SyncDb()
        {
            
        }

        public ArtistTrackCount GetMostPopularArtist()
        {
            return DataForMostPopularArtist;
        }

        protected ArtistTrackCount DataForMostPopularArtist { get; set; }

        public List<Artist> GetArgsForSave()
        {
            return _argsForSave;
        }

        public void VerifySaveWasntCalled()
        {
            Assert.IsFalse(_saveWasCalled);
        }
    }
}