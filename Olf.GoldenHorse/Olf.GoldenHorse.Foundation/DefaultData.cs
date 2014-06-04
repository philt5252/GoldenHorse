using System;
using System.IO;

namespace Olf.GoldenHorse.Foundation
{
    public static class DefaultData
    {
        public const string ProjectSuiteExtension = ".ghps";
        public const string TestExtension = ".ghtest";
        public const string LogExtension = ".ghlog";

        public static string GoldenHorseProjectsLocation
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GoldenHorseProjects");
            }
        }

        
    }
}