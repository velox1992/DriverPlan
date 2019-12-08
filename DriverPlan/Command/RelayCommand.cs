using System;
using System.Windows.Input;

namespace DriverPlan.Command
{
    public class RelayCommand : ICommand
    {

        protected Action<object> FExecute;
        protected Func<object, bool> FCanExecute;

        public RelayCommand(Action<object> _Execute, Func<object, bool> _CanExecute)
        {
            FExecute = _Execute;
            FCanExecute = _CanExecute;
        }


        public bool CanExecute(object _Parameter)
        {
            return (FCanExecute != null && FCanExecute(_Parameter));
        }

        public void Execute(object _Parameter)
        {
            FExecute?.Invoke(_Parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
