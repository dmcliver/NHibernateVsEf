using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Transactions;
using NHibernateVsEf.Annotations;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.Repositories.NHibernate;

namespace NHibernateVsEf.Importer
{
    public class ProfileDataImporter : IDataImporter
    {
        private readonly IUserProfileRepositoryNh _repositoryNh;

        public ProfileDataImporter([NotNull] IUserProfileRepositoryNh repositoryNh)
        {
            if (repositoryNh == null) throw new ArgumentNullException("repositoryNh");
            _repositoryNh = repositoryNh;
        }

        public ProfileDataImporter() : this(new UserProfileRepositoryNh()) { }

        public void Import(string[] lines, BackgroundWorker worker)
        {
            IEnumerable<string> linesWithNoHeader = lines.Skip(1);
            int i = 0;
            using (var tx = new TransactionScope())
            {
                foreach (var line in linesWithNoHeader)
                {
                    i++;
                    string[] fields = line.Split(new[] { "\t" }, StringSplitOptions.None);

                    if (fields.Length < 5)
                        throw new InvalidFieldLengthException("The amount of tab delimited fields must be at least 5");

                    UserProfile profile = new UserProfile
                    {
                        Id = fields[0],
                        Gender = ParseGender(fields[1]),
                        Age = ParseAge(fields[2]),
                        Country = fields[3]
                    };

                    _repositoryNh.Save(profile);

                    if (i%20 == 0)
                    {
                        worker.ReportProgress(i);
                        _repositoryNh.SyncDb();
                    }
                }
                tx.Complete();
            }
        }

        private static int ParseAge(string ageField)
        {
            if (string.IsNullOrEmpty(ageField))
                return 0;
            return Int32.Parse(ageField);
        }

        private Gender? ParseGender(string genderField)
        {
            if (genderField == "m")
                return Gender.Male;
            if (genderField == "f")
                return Gender.Female;
            return null;
        }
    }
}