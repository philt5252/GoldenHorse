
using System;
using System.Collections.Generic;
using System.Linq;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestItemEditorViewModel : ITestItemEditorViewModel
    {
        private readonly TestItem testItem;
        public ITestObjectEditorViewModel TestObjectEditorViewModel { get; protected set; }
        public ITestOperationEditorViewModel TestOperationEditorViewModel { get; protected set; }
        public ITestParameterEditorViewModel TestParameterEditorViewModel { get; protected set; }
        public ITestDescriptionEditorViewModel TestDescriptionEditorViewModel { get; protected set; }


        public TestItemEditorViewModel(TestItem testItem,
            ITestObjectEditorViewModelFactory testObjectEditorViewModelFactory,
            ITestOperationEditorViewModelFactory testOperationEditorViewModelFactory,
            ITestParameterEditorViewModelFactory testParameterEditorViewModelFactory,
            ITestDescriptionEditorViewModelFactory testDescriptionEditorViewModelFactory)
        {
            this.testItem = testItem;

            TestObjectEditorViewModel = testObjectEditorViewModelFactory.Create(testItem);
            TestOperationEditorViewModel = testOperationEditorViewModelFactory.Create(testItem);
            TestParameterEditorViewModel = testParameterEditorViewModelFactory.Create(testItem);
            TestDescriptionEditorViewModel = testDescriptionEditorViewModelFactory.Create(testItem);
        }
    }
}