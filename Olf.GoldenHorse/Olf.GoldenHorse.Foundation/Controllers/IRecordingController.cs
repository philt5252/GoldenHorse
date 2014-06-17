using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.Controllers
{
    public interface IRecordingController
    {
        void ShowRecord();
        void StopRecord();
        void DoValidation(TestItem testItem);
        void PauseRecord();
        void ResumeRecorder();
        void AppendToTest(Test test);
        void AppendToStart(Test test);
        void AppendAtIndex(Test test, int index);
    }
}