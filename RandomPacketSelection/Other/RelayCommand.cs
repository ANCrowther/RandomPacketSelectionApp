using System;
using System.Windows.Input;

namespace RandomPacketSelection
{
    // Manages the commands assigned to the 3 buttons in the program.
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
            DoWork(); ;
        }
    }
}
