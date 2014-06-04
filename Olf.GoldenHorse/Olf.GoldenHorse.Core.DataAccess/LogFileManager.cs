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
    public class LogFileManager : ILogFileManager
    {
        private XmlSerializer serializer;

        public LogFileManager()
        {
            serializer = new XmlSerializer(typeof(Log));
        }

        public void Save(Log log)
        {
            var logPath = ProjectSuiteManager.GetLogFolder(log);

            string filePath = Path.Combine(logPath, log.Name + DefaultData.LogExtension);
            using (FileStream fileStream = File.Create(filePath))
            {
                serializer.Serialize(fileStream, log);

                fileStream.Flush();
            }

            log.Owner.RefreshLogFiles();
        }

        public Log Open(string filePath)
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                Log log = serializer.Deserialize(fileStream) as Log;

                return log;
            }
        }
    }
}