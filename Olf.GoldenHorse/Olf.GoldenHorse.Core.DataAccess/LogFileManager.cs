using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading;
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
            List<Type> types = new List<Type>();

            FileInfo fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);

            foreach (string assemblyFile in Directory.EnumerateFiles(fileInfo.Directory.FullName)
                .Where(f => f.EndsWith(".exe") || f.EndsWith(".dll")))
            {
                if (assemblyFile.Contains("Olf.Common.Extensions"))
                    continue;

                types.AddRange(Assembly.LoadFile(assemblyFile).GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(ScreenshotAdornment))));
            }

            serializer = new XmlSerializer(typeof(Log), types.ToArray());
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

        public void Rename(ProjectFile logFile, string newName)
        {
            
            FileInfo fileInfo = new FileInfo(logFile.FilePath);
            string oldDirPath = fileInfo.Directory.FullName;
            string newDirPath = oldDirPath.Replace(logFile.Name.Replace(DefaultData.LogExtension, ""), newName);
            string newPath = Path.Combine(newDirPath, newName + DefaultData.LogExtension);

            //if (!Directory.Exists(newDirPath))
            //    Directory.CreateDirectory(newDirPath);

            File.Move(logFile.FilePath, Path.Combine(oldDirPath, newName + DefaultData.LogExtension));

            try
            {
                Directory.Move(oldDirPath, newDirPath);
            }
            catch (Exception e)
            {
                
            }
            
            //Directory.Delete(oldDirPath);

            logFile.FilePath = newPath;
            logFile.Name = newName + DefaultData.LogExtension; 
            
            Log log = Open(newPath);
            log.Owner = logFile.Project;
            log.Name = newName;


            Save(log);
        }
    }
}