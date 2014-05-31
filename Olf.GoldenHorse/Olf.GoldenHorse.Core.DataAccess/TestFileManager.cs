using System;
using System.Collections.Generic;
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
            List<Type> types = new List<Type>();

            FileInfo fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);

            foreach (string assemblyFile in Directory.EnumerateFiles(fileInfo.Directory.FullName)
                .Where(f => f.EndsWith(".exe") || f.EndsWith(".dll")))
            {
                types.AddRange(Assembly.LoadFile(assemblyFile).GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(MappedItem))
                        || t.IsSubclassOf(typeof(TestItem))
                        || t.IsSubclassOf(typeof(Operation))
                        || t.IsSubclassOf(typeof(OperationParameterValue))
                        || t.IsSubclassOf(typeof(ScreenshotAdornment))));
            }

            serializer = new XmlSerializer(typeof(Test), types.ToArray());
        }

        public Test CreateTestForProject(Project project)
        {
            var testsPath = ProjectSuiteManager.GetTestsFolder(project);

            string defaultTestName = DefaultNameHelper.GetDefaultName(testsPath, "Test", DefaultNameHelper.CheckType.File);

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

        public void Save(Test test)
        {
            var testsPath = ProjectSuiteManager.GetTestsFolder(test.Project);

            string filePath = Path.Combine(testsPath, test.Name + DefaultData.TestExtension);
            using (FileStream fileStream = File.Create(filePath))
            {
                serializer.Serialize(fileStream, test);

                fileStream.Flush();
            }
        }

        public Test Open(string filePath)
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                return serializer.Deserialize(fileStream) as Test;
            }
        }
    }
}