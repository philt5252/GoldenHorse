using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TestForGolden
{
    public class XmlFileWriter
    {
        private readonly Project project;

        public XmlFileWriter(Project project)
        {
            this.project = project;
        }

        public void SaveProject()
        {
            XmlSerializer serializer = new XmlSerializer(project.GetType());
            string projectPath = Path.Combine(project.ProjectFolder, project.Name + ".ghproj");
            Directory.CreateDirectory(project.ProjectFolder);

            using (FileStream fileStream = File.Create(projectPath))
            {
                serializer.Serialize(fileStream, project);

                fileStream.Flush();
            } 
        }

        public void Write(AppManager appManager)
        {
            Type[] testItemTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(MappedItem)) 
                    || t.IsSubclassOf(typeof(Operation))).ToArray();

            XmlSerializer serializer = new XmlSerializer(appManager.GetType(), testItemTypes);

            string appManagerDir = Path.Combine(project.ProjectFolder, project.AppManagerFolder);
            Directory.CreateDirectory(appManagerDir);

            string appManagerPath = Path.Combine(appManagerDir, "AppManager.gham");

            using (FileStream fileStream = File.Create(appManagerPath))
            {
                serializer.Serialize(fileStream, appManager);

                fileStream.Flush();
            } 
        }

        public void Write(Test test)
        {
            Type[] testItemTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(MappedItem))
                    || t.IsSubclassOf(typeof(TestItem))
                    || t.IsSubclassOf(typeof(Operation))
                    || t.IsSubclassOf(typeof(OperationParameterValue))
                    || t.IsSubclassOf(typeof(ScreenshotAdornment))).ToArray();

            XmlSerializer serializer = new XmlSerializer(test.GetType(), testItemTypes);

            var testPath = CreateTestPath(test.Name);

            using (FileStream fileStream = File.Create(testPath))
            {
                serializer.Serialize(fileStream, test);

                fileStream.Flush();
            } 
        }

        private string CreateTestPath(string testName)
        {
            string testDir = Path.Combine(project.ProjectFolder, project.TestsFolder, testName);
            if (!Directory.Exists(testDir))
                Directory.CreateDirectory(testDir);

            string testPath = Path.Combine(testDir, testName + ".ghtest");
            return testPath;
        }

        public Test Read(string testName)
        {
            Type[] testItemTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(MappedItem))
                    || t.IsSubclassOf(typeof(TestItem))
                    || t.IsSubclassOf(typeof(Operation))
                    || t.IsSubclassOf(typeof(OperationParameterValue))
                    || t.IsSubclassOf(typeof(ScreenshotAdornment))).ToArray();

            var testPath = CreateTestPath(testName);

            XmlSerializer serializer = new XmlSerializer(typeof(Test), testItemTypes);

            using (FileStream fileStream = File.OpenRead(testPath))
            {
                return serializer.Deserialize(fileStream) as Test;
            }
        }

        public AppManager ReadAppManager()
        {
            Type[] testItemTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(MappedItem))).ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(AppManager), testItemTypes);

            using (FileStream fileStream = File.OpenRead("saveFileAppManager.xml"))
            {
                return serializer.Deserialize(fileStream) as AppManager;
            }
        }
    }
}