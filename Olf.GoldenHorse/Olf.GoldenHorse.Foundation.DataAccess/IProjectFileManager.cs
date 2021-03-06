﻿using System.Xml.Linq;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.DataAccess
{
    public interface IProjectFileManager
    {
        Project Open(string filePath);
        void Save(Project project);
        void Create(Project project);
    }
}