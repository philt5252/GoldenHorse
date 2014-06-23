
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestOperationsViewModel : ITestOperationsViewModel
    {
        public IOperationViewModel[] Operations { get; protected set; }
        public IOperationViewModel SeletedOperation { get; protected set; }

        public TestOperationsViewModel(Test test, IEnumerable<Func<Test, IOperationViewModel>> operations)
        {
            Operations = operations.Select(t => t(test)).ToArray();
        }
    }
}