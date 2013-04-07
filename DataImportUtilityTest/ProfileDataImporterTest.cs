using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NHibernateVsEf.Core.Domain;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Importer;
using NUnit.Framework;

namespace DataImportUtilityTest
{
    [TestFixture]
    public class ProfileDataImporterTest
    {
        private const string UserId1 = "user_000001";
        private const string UserId3 = "user_000003";
        private const string UserId2 = "user_000002";
        private const int Age3 = 22;
        
        [Test]
        public void Import_WithProperlyFormattedStringArray_SavesToRepository()
        {
            UserProfileRepositoryNhStub repositoryNhStub = new UserProfileRepositoryNhStub();
            var importer = new ProfileDataImporter(repositoryNhStub);
            
            var lines = BuildLines();
            
            importer.Import(lines, new BackgroundWorker());

            Assert.That(repositoryNhStub.UsersPassedIntoSaveMethod.Count, Is.EqualTo(3));

            UserProfile userProfile1 = repositoryNhStub.UsersPassedIntoSaveMethod[0];
            UserProfile userProfile2 = repositoryNhStub.UsersPassedIntoSaveMethod[1];
            UserProfile userProfile3 = repositoryNhStub.UsersPassedIntoSaveMethod[2];

            UserProfileAssertion(userProfile1, UserId1, Gender.Male, 0, "Japan");
            UserProfileAssertion(userProfile2, UserId2, Gender.Female, 0, "Peru");
            UserProfileAssertion(userProfile3, UserId3, null, 22, "Australia");
        }

        [Test]
        [ExpectedException(ExpectedException = typeof(InvalidFieldLengthException))]
        public void Import_WithBadFormattedStringArray_ThrowsException()
        {
            UserProfileRepositoryNhStub repositoryNhStub = new UserProfileRepositoryNhStub();
            var importer = new ProfileDataImporter(repositoryNhStub);

            string[] lines = new[]{"Header", "bad data..bad data...bad data!!!"};

            importer.Import(lines, new BackgroundWorker());
        }

        private static string[] BuildLines()
        {
            var lines = new[]
            {
                "Header",
                UserId1 + "\tm\t\tJapan\tAug 13,2006",
                UserId2 + "\tf\t\tPeru\tAug 13,2006",
                UserId3 + "\t\t" + Age3 + "\tAustralia\tAug 13,2006",
            };
            return lines;
        }

        private static void UserProfileAssertion(UserProfile userProfile1, string userId, Gender? gender,int age, string country)
        {
            Assert.That(userProfile1.Id, Is.EqualTo(userId));
            Assert.That(userProfile1.Gender, Is.EqualTo(gender));
            Assert.That(userProfile1.Age, Is.EqualTo(age));
            Assert.That(userProfile1.Country, Is.EqualTo(country));
        }
    }
}