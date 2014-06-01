using System.Drawing;
using TestStack.White.UIItems;

namespace Olf.GoldenHorse.Foundation.Services
{
    public interface IExternalAppInfoManager
    {
        string GetForegroundWindowProcessName();
        string GetForegroundWindowName();

        Point GetForegroundWindowLocalizedPoint();

        UIItem GetControl(Point point);
    }
}