using System.Windows.Input;

namespace Clinic.ViewModel.Utils
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action<object?> execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = (obj) => execute();
            _canExecute = canExecute;
        }

        private Action<object?> _execute;
        public void Execute(object? obj)
        {
            _execute(obj);
        }

        private Func<bool>? _canExecute;
        public bool CanExecute(object? obj)
        {
            return this._canExecute == null || this._canExecute();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
