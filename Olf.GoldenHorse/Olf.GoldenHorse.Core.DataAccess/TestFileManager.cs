using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Olf.GoldenHorse.Foundation;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Core.DataAccess
{
    public class TestFileManager : ITestFileManager
    {
        private XmlSerializer serializer;

        public TestFileManager()
        {
            Type[] testItemTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(MappedItem))
                    || t.IsSubclassOf(typeof(TestItem))
                    || t.IsSubclassOf(typeof(Operation))
                    || t.IsSubclassOf(typeof(OperationParameterValue))
                    || t.IsSubclassOf(typeof(ScreenshotAdornment))).ToArray();

            serializer = new XmlSerializer(typeof(Test), testItemTypes);
        }

        public Test CreateTestForProject(Project project)
        {
            var testsPath = ProjectSuiteManager.GetTestsFolder(project);

            string defaultTestName = DefaultNameHelper.GetDefaultName(testsPath, "Test");

            Test test = new Test();
            test.Name = defaultTestName;
            test.Project = project;

            string filePath = Path.Combine(testsPath, defaultTestName + DefaultData.TestExtension);
            using (FileStream fileStream = File.Create(filePath))
            {
                serializer.Serialize(fileStream, test);

                fileStream.Flush();
            }

            return test;
        }
    }
}