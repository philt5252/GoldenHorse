using System.Drawing;

namespace Olf.GoldenHorse.Foundation.Services
{
    public interface IExternalAppInfoManager
    {
        string GetForegroundWindowProcessName();
        string GetForegroundWindowName();

        Point GetForegroundWindowLocalizedPoint()//for when window is already focused
            ;
    }
}