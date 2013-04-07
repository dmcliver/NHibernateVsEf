using System;
using System.Text.RegularExpressions;

namespace NHibernateVsEf.Importer
{
    public class DataImporterFactory : IDataImporterFactory
    {
        public IDataImporter Create(string fileName)
        {
            string userTag = Regex.Match(fileName, "profile").Value;
            string musicTag = Regex.Match(fileName, "artname").Value;
            userTag = userTag.Capitalize();
            musicTag = musicTag.Capitalize();

            Type type = Type.GetType("NHibernateVsEf.Importer." + userTag + musicTag + "DataImporter");
            object instance;
            try
            {
                instance = Activator.CreateInstance(type);
            }
            catch (ArgumentNullException ex)
            {
                throw new IncorrectFileNameException("The file should be named either userid-profile.tsv or userid-timestamp-artid-artname-traid-traname.tsv", ex);
            }
            return instance as IDataImporter;
        }
    }
}