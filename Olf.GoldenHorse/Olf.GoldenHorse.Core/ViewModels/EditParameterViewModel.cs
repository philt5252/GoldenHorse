
using System;
using System.Linq;
using System.Windows.Input;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class EditParameterViewModel : IEditParameterViewModel
    {
        private readonly OperationParameter parameter;
        public string[] ValidationModes { get; protected set; }

        public string SelectedValidationMode
        {
            get
            {
                return parameter.Mode.ToString();
            }
            set
            {
                parameter.Mode = (OperationParameterValueMode)Enum.Parse(typeof(OperationParameterValueMode), value);
            }
        }

        public string Value
        {
            get
            {
                return parameter.Value.ToString();
            }
            set
            {
                parameter.Value = value;
            }
        }

        public string Name { get { return parameter.Name; } }

        public EditParameterViewModel(OperationParameter parameter)
        {
            this.parameter = parameter;
            
            ValidationModes = Enum.GetNames(typeof (OperationParameterValueMode))
                .ToArray();
        }
    }
}