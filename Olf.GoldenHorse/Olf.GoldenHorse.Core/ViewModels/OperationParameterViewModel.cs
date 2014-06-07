
using System.Windows.Input;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class OperationParameterViewModel : IOperationParameterViewModel
    {
        private readonly OperationParameter parameter;

        public string Name { get { return parameter.Name; } }
        public string Value { get { return parameter.Value.ToString(); } }

        public ICommand EditParameterCommand { get; protected set; }

        public OperationParameterViewModel(OperationParameter parameter)
        {
            this.parameter = parameter;
        }
    }
}