using System.ComponentModel;

namespace NHibernateVsEf.Services
{
    public interface IDataImportService
    {
        void ImportData(string fileName, BackgroundWorker worker);
    }
}