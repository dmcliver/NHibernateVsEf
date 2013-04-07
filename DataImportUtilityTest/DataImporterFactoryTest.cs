using NHibernateVsEf.Importer;
using NUnit.Framework;

namespace DataImportUtilityTest
{
    [TestFixture]
    public class DataImporterFactoryTest
    {
        [Test]
        public void Create_WithFilenameContainingProfile_CreatesProfileImporter()
        {
            var factory = new DataImporterFactory();
            IDataImporter dataImporter = factory.Create("my-proprofiler-file.ext");

            Assert.NotNull(dataImporter);
            Assert.That(dataImporter.GetType(), Is.EqualTo(typeof(ProfileDataImporter)));
        }

        [Test]
        public void Create_WithFilenameContainingTrack_CreatesTrackImporter()
        {
            var factory = new DataImporterFactory();
            IDataImporter dataImporter = factory.Create("my-artname-track-file.fil");

            Assert.NotNull(dataImporter);
            Assert.That(dataImporter.GetType(), Is.EqualTo(typeof(ArtnameDataImporter)));
        }

        [Test]
        [ExpectedException(typeof(IncorrectFileNameException), ExpectedMessage = "The file should be named either userid-profile.tsv or userid-timestamp-artid-artname-traid-traname.tsv")]
        public void Create_WithFilenameContainingNeitherTrackOrProfile_ThrowsException()
        {
            var factory = new DataImporterFactory();
            factory.Create("my-i-am-a-little-teapot-file.fuk");
        }
    }
}
