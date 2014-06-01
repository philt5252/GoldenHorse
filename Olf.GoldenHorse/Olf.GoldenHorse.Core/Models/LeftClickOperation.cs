using Olf.GoldenHorse.Foundation.Services;
using TestStack.White.UIItems;

namespace Olf.GoldenHorse.Core.Models
{
    public class LeftClickOperation : ClickOperation
    {
        public override string Name
        {
            get { return "Left Click"; }
        }

        public override void Play(string processName, string windowName, string controlName)
        {
            IUIItem uiItem = AppPlaybackService.GetControl(processName, windowName, controlName);

            uiItem.Click();
        }
    }
}