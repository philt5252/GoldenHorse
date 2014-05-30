
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Foundation.Factories.ViewModels
{
    public interface IRecorderViewModelFactory
    {
        IRecorderViewModel Create(IRecorder recorder);
    }
}