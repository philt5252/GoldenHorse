using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.Controllers
{
    public interface ITrainingController
    {
        void Start(Test test);
        void Stop();
    }
}