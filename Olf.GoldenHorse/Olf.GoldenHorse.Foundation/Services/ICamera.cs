using System.Drawing;

namespace Olf.GoldenHorse.Foundation.Services
{
    public interface ICamera
    {
        Bitmap Capture(ScreenCaptureMode screenCaptureMode = ScreenCaptureMode.Window);
    }

    public enum ScreenCaptureMode
    {
        Screen,
        Window
    }
}