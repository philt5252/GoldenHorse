﻿using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.DataAccess
{
    public interface ITestFileManager
    {
        Test CreateTestForProject(Project project);
    }
}