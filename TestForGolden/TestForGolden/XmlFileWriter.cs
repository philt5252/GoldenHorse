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
        public void Write(Test test)
        {
            Type[] testItemTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(TestItem))).ToArray();

            XmlSerializer serializer = new XmlSerializer(test.GetType(), testItemTypes);

            using (FileStream fileStream = File.Create("saveFile.xml"))
            {
                serializer.Serialize(fileStream, test);

                fileStream.Flush();
            } 
        }

        public Test Read()
        {
            Type[] testItemTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(TestItem))).ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(Test), testItemTypes);

            using (FileStream fileStream = File.OpenRead("saveFile.xml"))
            {
                return serializer.Deserialize(fileStream) as Test;
            } 
        }
    }
}