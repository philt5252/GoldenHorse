using System;
using System.IO;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class TestNode : DisplayNode
    {
        private readonly ProjectFile projectFile;
        private readonly ITestController testController;
        private readonly ITestFileManager testFileManager;

        public override string Name
        {
            get { return projectFile.Name; }
        }

        public TestNode(ProjectFile projectFile, ITestController testController,
            ITestFileManager testFileManager)
        {
            this.projectFile = projectFile;
            this.testController = testController;
            this.testFileManager = testFileManager;

            DefaultCommand = new DelegateCommand(ExecuteDefaultCommand);
        }

        protected virtual void ExecuteDefaultCommand()
        {
            Test test = testFileManager.Open(projectFile.FilePath);
            test.Project = projectFile.Project;
            testController.ShowTest(test);
        }
    }
}