
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Foundation.Factories.ViewModels
{
    public interface ITrainingMainViewModelFactory
    {
        ITrainingMainViewModel Create(Test test);
    }
}