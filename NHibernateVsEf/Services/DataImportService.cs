using System;
using System.ComponentModel;
using System.IO;
using NHibernateVsEf.Annotations;
using NHibernateVsEf.Importer;

namespace NHibernateVsEf.Services
{
    public class DataImportService : IDataImportService
    {
        private readonly IDataImporterFactory _factory;

        public DataImportService([NotNull] IDataImporterFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            _factory = factory;
        }

        public DataImportService():this(new DataImporterFactory()){}
        
        public void ImportData(string fileName, BackgroundWorker worker)
        {
            IDataImporter importer = _factory.Create(fileName);
            importer.Import(File.ReadAllLines(fileName), worker);
        }
    }
}