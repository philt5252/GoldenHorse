using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.Services
{
    public interface IRecorder
    {
        Test CurrentTest { get; }
        int InsertPosition { get; set; }
        void Record();
        void Stop();
        void Pause();
    }
}