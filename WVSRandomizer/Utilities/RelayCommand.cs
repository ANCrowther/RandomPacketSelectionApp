using System;
using System.Windows.Input;

namespace WVSRandomizer.Utilities
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        readonly Action DoWork;

        public RelayCommand(Action work)
        {
            DoWork = work;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DoWork();
        }
    }
}
