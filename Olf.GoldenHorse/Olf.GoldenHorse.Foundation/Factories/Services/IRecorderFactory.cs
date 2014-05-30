using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Foundation.Factories.Services
{
    public interface IRecorderFactory
    {
        IRecorder Create(Test test);
    }
}