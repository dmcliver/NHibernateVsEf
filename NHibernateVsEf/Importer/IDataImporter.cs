using System.ComponentModel;

namespace NHibernateVsEf.Importer
{
    public interface IDataImporter
    {
        void Import(string[] lines, BackgroundWorker worker);
    }
}