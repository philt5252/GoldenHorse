
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class VariableManagerViewModel : IVariableManagerViewModel
    {
        private readonly Test test;

        public ObservableCollection<Variable> Variables { get; protected set; } 
        public static VariableType[] Types { get; protected set; }

        static VariableManagerViewModel()
        {
            Types = Enum.GetValues(typeof (VariableType)).Cast<VariableType>().ToArray();
        }

        public VariableManagerViewModel(Test test)
        {
            this.test = test;
            Variables = test.Variables;
            
        }
    }
}