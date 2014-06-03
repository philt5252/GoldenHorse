using System.Drawing;
using Olf.Automation;
using Olf.GoldenHorse.Foundation.Models;
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

        public override void Play(MappedItem mappedItem)
        {
            AppProcess process = AppManager.GetProcess(mappedItem);
            MappedItem window = AppManager.GetWindow(mappedItem);

            IUIItem uiItem = AppPlaybackService.GetControl(process, window, mappedItem);
            Point clickPoint = this.GetClickPoint();
            Cursor.LeftClick(new Point((int)uiItem.Bounds.X + clickPoint.X, (int)uiItem.Bounds.Y + clickPoint.Y));

            //uiItem.Click();
        }
    }
}