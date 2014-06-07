using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.Controllers
{
    public interface IRecordingController
    {
        void ShowRecord();
        void StopRecord();
        void DoValidation(OnScreenValidation onScreenValidation);
        void PauseRecord();
        void ResumeRecorder();
    }
}