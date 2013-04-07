using System.ComponentModel;
using NHibernateVsEf.Annotations;

namespace NHibernateVsEf.ViewModels
{
    public abstract class ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}