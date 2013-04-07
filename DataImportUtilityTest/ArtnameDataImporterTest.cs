using System.Collections.Generic;
using System.ComponentModel;
using NHibernateVsEf.Core.Domain;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Importer;
using NUnit.Framework;

namespace DataImportUtilityTest
{
    [TestFixture]
    public class ArtnameDataImporterTest
    {
        private UserTrackRepositoryNhMock _userTrackRepositoryNhMock;
        private ArtistRepositoryNhStub _artistRepositoryNhStub;
        private UserProfileRepositoryNhStub _userRepositoryNhStub;
        private TrackRepositoryNhStub _trackRepositoryNhStub;

        [SetUp]
        public void StartMeUp()
        {
            _trackRepositoryNhStub = new TrackRepositoryNhStub();
            _userRepositoryNhStub = new UserProfileRepositoryNhStub();
            _artistRepositoryNhStub = new ArtistRepositoryNhStub();
            _userTrackRepositoryNhMock = new UserTrackRepositoryNhMock();
        }

        [Test]
        public void Import_WithArtistAndTrackNotFoundInDb_SavesNewArtistAndTrackInDb()
        {
            string[] lines = new[] 
            { 
                "user_00001\t2009-05-10\t2kde9qq3-3ks9-a1ks-21ka0-82ms81as9w\tUnderworld\t3mf93a0-e92d-39j4-329s-md83msk9d\tUfo" 
            };

            _userRepositoryNhStub.OnFindByIdReturn(new UserProfile());

            var importer = new ArtnameDataImporter(_trackRepositoryNhStub, _userRepositoryNhStub, _artistRepositoryNhStub, _userTrackRepositoryNhMock);
            importer.Import(lines, new BackgroundWorker());

            List<Track> args = _trackRepositoryNhStub.GetArgsForSave();
            Assert.That(args.Count, Is.EqualTo(1));
            string title = args[0].Title;

            List<Artist> artists = _artistRepositoryNhStub.GetArgsForSave();
            Assert.That(artists.Count, Is.EqualTo(1));
            string name = artists[0].Name;

            Assert.That(name, Is.EqualTo("Underworld"));
            Assert.That(title, Is.EqualTo("Ufo"));
            _userTrackRepositoryNhMock.VerifySaveWasCalled();
        }

        [Test]
        public void Import_WithArtistAndTrackFoundInDb_SavesOnlyUserTrackInfoInDb()
        {
            Track track1 = new Track();
            Artist artist = new Artist();

            string[] lines = new[] 
            { 
                "user_00001\t2009-05-10\t2kde9qq3-3ks9-a1ks-21ka0-82ms81as9w\tUnderworld\t3mf93a0-e92d-39j4-329s-md83msk9d\tUfo" 
            };

            UserProfile userProfile = new UserProfile();
            _userRepositoryNhStub.OnFindByIdReturn(userProfile);
            
            _trackRepositoryNhStub.OnFindByTitleReturn(track1);
            _artistRepositoryNhStub.OnFindByNameReturn(artist);

            var importer = new ArtnameDataImporter(_trackRepositoryNhStub, _userRepositoryNhStub, _artistRepositoryNhStub, _userTrackRepositoryNhMock);
            importer.Import(lines, new BackgroundWorker());

            List<UserTrack> userTracks = _userTrackRepositoryNhMock.GetArgsForSave();
            Assert.That(userTracks.Count, Is.EqualTo(1));
            UserTrack userTrack = userTracks[0];

            _trackRepositoryNhStub.VerifySaveWasntCalled();
            _artistRepositoryNhStub.VerifySaveWasntCalled();
            _userTrackRepositoryNhMock.VerifySaveWasCalled();
            
            Assert.That(userTrack.Track, Is.EqualTo(track1));
            Assert.That(userTrack.UserProfile, Is.EqualTo(userProfile));
        }

        [Test]
        [ExpectedException(ExpectedException = typeof(InvalidFieldLengthException))]
        public void Import_WithInvalidFieldLength_ThrowsError()
        {
            string[] lines = new[] 
            { 
                "user_00001\t2009-05-102kde9qq3-3ks9-a1ks-21ka0-82ms81as9w\tUnderworld\t3mf93a0-e92d-39j4-329s-md83msk9d\tUfo" 
            };

            var importer = new ArtnameDataImporter(_trackRepositoryNhStub, _userRepositoryNhStub, _artistRepositoryNhStub, _userTrackRepositoryNhMock);
            importer.Import(lines, new BackgroundWorker());
        }
    }
}