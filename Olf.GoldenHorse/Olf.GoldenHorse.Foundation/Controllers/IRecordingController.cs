using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.Controllers
{
    public interface IRecordingController
    {
        void ShowRecord(Test test);
        void StopRecord();
    }
}