using System;
using System.Windows.Input;

namespace NHibernateVsEf.ViewModels
{
    public class ActionCommand : ICommand
    {
        private readonly Action _act;

        public ActionCommand(Action act)
        {
            if (act == null) throw new ArgumentNullException("act");
            _act = act;
        }

        public void Execute(object parameter)
        {
            _act();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}