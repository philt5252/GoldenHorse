using System;

namespace Olf.GoldenHorse.Foundation
{
    public static class DefaultData
    {
        public const string ProjectSuiteExtension = ".ghps";
        public static string GoldenHorseProjectsLocation
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "GoldenHorseProjects";
            }
        }
    }
}