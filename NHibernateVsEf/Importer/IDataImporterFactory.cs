namespace NHibernateVsEf.Importer
{
    public interface IDataImporterFactory
    {
        IDataImporter Create(string fileName);
    }
}