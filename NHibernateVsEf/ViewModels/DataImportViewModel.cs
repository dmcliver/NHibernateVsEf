using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using NHibernateVsEf.Services;

namespace NHibernateVsEf.ViewModels
{
    public class DataImportViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IDataImportService _dataImportService;
        private string _errorLabel;
        private readonly BackgroundWorker _worker;
        private string _progress;
        private bool _hasErrors;

        public ICommand DisplayDialogCmd { get; private set; }

        public DataImportViewModel(IDataImportService dataImportService)
        {
            if (dataImportService == null) throw new ArgumentNullException("dataImportService");
            _dataImportService = dataImportService;

            DisplayDialogCmd = new ActionCommand(DisplayDialog);
            _worker = new BackgroundWorker { WorkerReportsProgress = true };
            _worker.DoWork += worker_DoWork;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            _worker.ProgressChanged += worker_ProgressChanged;
        }
        
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage.ToString(CultureInfo.InvariantCulture);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(!_hasErrors)
                MessageBox.Show("Finished");
        }

        public DataImportViewModel() : this(new DataImportService()) { }

        private void DisplayDialog()
        {
            ErrorLabel = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "TSV Files (*.tsv)|*.tsv" };
            bool? result = openFileDialog.ShowDialog();

            if (result.Value && openFileDialog.FileName.EndsWith(".tsv"))
            {
                _worker.RunWorkerAsync(openFileDialog.FileName);
            }
            else if (result.Value)
                ErrorLabel = "Please enter a tsv file";
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _hasErrors = false;
            try
            {
                Progress = "Initializing import, please wait...";
                _dataImportService.ImportData(e.Argument.ToString(), _worker);
            }
            catch (Exception ex)
            {
                _hasErrors = true;
                MessageBox.Show(ex.Message);
            }
        }

        public string ErrorLabel
        {
            get { return _errorLabel; }
            set
            {
                _errorLabel = value;
                OnPropertyChanged("ErrorLabel");
            }
        }

        public string Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged("Progress");
            }
        }
    }
}