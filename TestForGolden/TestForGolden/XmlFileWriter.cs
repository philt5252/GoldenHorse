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

        public XmlFileWriter()
        {
        }

        public void SaveProject()
        {
            XmlSerializer serializer = new XmlSerializer(ProjectManager.CurrentProject.GetType());
            var projectPath = ProjectManager.GetProjectPath();

            using (FileStream fileStream = File.Create(projectPath))
            {
                serializer.Serialize(fileStream, ProjectManager.CurrentProject);

                fileStream.Flush();
            } 
        }

        

        public void Write(AppManager appManager)
        {
            Type[] testItemTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(MappedItem)) 
                    || t.IsSubclassOf(typeof(Operation))).ToArray();

            XmlSerializer serializer = new XmlSerializer(appManager.GetType(), testItemTypes);

            var appManagerPath = ProjectManager.GetAppManagerPath();

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

            var testPath = ProjectManager.GetTestPath(test.Name);

            using (FileStream fileStream = File.Create(testPath))
            {
                serializer.Serialize(fileStream, test);

                fileStream.Flush();
            } 
        }

        public Test Read(string testName)
        {
            Type[] testItemTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(MappedItem))
                    || t.IsSubclassOf(typeof(TestItem))
                    || t.IsSubclassOf(typeof(Operation))
                    || t.IsSubclassOf(typeof(OperationParameterValue))
                    || t.IsSubclassOf(typeof(ScreenshotAdornment))).ToArray();

            var testPath = ProjectManager.GetTestPath(testName);

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

            var appManagerPath = ProjectManager.GetAppManagerPath();

            using (FileStream fileStream = File.OpenRead(appManagerPath))
            {
                return serializer.Deserialize(fileStream) as AppManager;
            }
        }
    }
}