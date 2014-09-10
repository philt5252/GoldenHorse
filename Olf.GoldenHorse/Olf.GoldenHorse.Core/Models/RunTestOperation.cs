using System;
using System.Linq;
using System.Threading;
using Microsoft.Practices.ServiceLocation;
using Olf.GoldenHorse.Foundation;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Core.Models
{
    public class RunTestOperation : Operation
    {
        private readonly ITestFileManager testFileManager;

        private OperationParameter testNameParam
        {
            get { return Parameters[0]; }
        }

        public override string Name
        {
            get { return "Run Test"; }
        }

        public string TestName
        {
            get { return testNameParam.Value.ToString(); }
            set
            {
                testNameParam.Value = value;
            }
        }

        public override string ParametersDescription
        {
            get { return TestName; }
        }

        public RunTestOperation()
        {
            this.testFileManager = ServiceLocator.Current.GetInstance<ITestFileManager>();
        }

        public RunTestOperation(ITestFileManager testFileManager)
        {
            this.testFileManager = testFileManager;
        }

        protected override OperationParameter[] SetParameters()
        {
            var param1 = new OperationParameter
            {
                Name = "TestName",
                Mode = OperationParameterValueMode.Constant
            };

            return new[] {param1};
        }

        public override string DefaultDescription(MappedItem control)
        {
            return string.Format("Run Test \"{0}\"", TestName);
        }

        public override bool Play(MappedItem control, Log log)
        {
            string testName = testNameParam.GetValue();
            string filePath =
                this.TestItem.Test.Project.TestFiles.First(
                    t => t.Name.Substring(0, t.Name.Length - DefaultData.TestExtension.Length).Equals(testName)).FilePath;

            Test test = testFileManager.Open(filePath);
            test.Project = TestItem.Test.Project;

            log.CreateLogItem(LogItemCategory.Event, string.Format("Running test: {0}", testName));
            log.StartLogItemChildren();
            test.Play(log);
            log.EndLogItemChildren();
            return true;
        }
    }
}