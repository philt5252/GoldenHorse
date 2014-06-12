using System;
using System.Drawing;
using System.IO;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation;
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
        private Test test;

        public override bool IsRenamable
        {
            get { return true; }
        }

        public override string Name
        {
            get { return projectFile.Name.Replace(DefaultData.TestExtension, ""); }
            set
            {
                if (Equals(projectFile.Name.Replace(DefaultData.TestExtension, ""), value))
                    return;

                testFileManager.Rename(projectFile, value);

                if (test != null)
                    test.Name = value;
            }
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
            test = testFileManager.Open(projectFile.FilePath);
            test.Project = projectFile.Project;
            testController.ShowTest(test);
        }
    }
}