using System;
using System.Windows.Input;

namespace WpfApplication1
{
    public class TestViewModel
    {
        public ICommand Test1Command { get; protected set; }
        public ICommand Test2Command { get; protected set; }
        public ICommand Test3Command { get; protected set; }

        public TestViewModel()
        {
            Test1Command = new DelegateCommand(ExecuteTest1Command);
            Test2Command = new DelegateCommand(ExecuteTest2Command);
            Test3Command = new DelegateCommand(ExecuteTest3Command);
        }

        private void ExecuteTest1Command()
        {
        }

        private void ExecuteTest2Command()
        {
        }

        private void ExecuteTest3Command()
        {
        }
    }

    public class DelegateCommand : ICommand
    {
        private readonly Action action;

        public DelegateCommand(Action action )
        {
            this.action = action;
        }

        public void Execute(object parameter)
        {
            action.Invoke();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}