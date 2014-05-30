using System;
using Olf.GoldenHorse.Foundation.Factories.Services;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Core.Factories.Services
{
    public class RecorderFactory : IRecorderFactory
    {
        private readonly Func<Test, IRecorder> createServiceFunc;

        public RecorderFactory(Func<Test, IRecorder> createServiceFunc)
        {
            this.createServiceFunc = createServiceFunc;
        }

        public IRecorder Create(Test test)
        {
            return createServiceFunc(test);
        }
    }
}