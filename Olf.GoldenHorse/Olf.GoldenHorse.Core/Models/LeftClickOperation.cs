using System;
using System.Drawing;
using System.IO;
using System.Windows.Automation;
using System.Windows.Forms;
using Olf.GoldenHorse.Core.Services;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using TestStack.White.UIItems;
using Cursor = Olf.Automation.Cursor;

namespace Olf.GoldenHorse.Core.Models
{
    public class LeftClickOperation : ClickOperation
    {
        public override string Name
        {
            get { return "Left Click"; }
        }

        public override bool Play(MappedItem mappedItem, Log log)
        {
            AppProcess process = AppManager.GetProcess(mappedItem);
            MappedItem window = AppManager.GetWindow(mappedItem);

            IUIItem uiItem = AppPlaybackService.GetControl(process, window, mappedItem, AppManager);
            Point clickPoint = this.GetClickPoint();
            Point globalPoint = new Point((int)uiItem.Bounds.X + clickPoint.X, (int)uiItem.Bounds.Y + clickPoint.Y);

            Cursor.Position = globalPoint;

            AutomationElement findWindowElement = ExternalAppInfoManager.GetWindowAutomationElement(uiItem);
            Screenshot screenshot = CreateScreenshot(log);
            screenshot.Adornments.Add(
                new ControlHighlightAdornment
                {
                    X = (int)uiItem.Bounds.X - (int)findWindowElement.Current.BoundingRectangle.X,
                    Y = (int)uiItem.Bounds.Y - (int)findWindowElement.Current.BoundingRectangle.Y,
                    Width = (int)uiItem.Bounds.Width,
                    Height = (int)uiItem.Bounds.Height
                });

            Cursor.LeftClick(globalPoint);
           

            string description = string.Format("The {0} was clicked with the left mouse button", mappedItem.Type);

            log.CreateLogItem(LogItemCategory.Event, description, screenshot);

            int screenshotX = globalPoint.X - (int)findWindowElement.Current.BoundingRectangle.X;
            int screenshotY = globalPoint.Y - (int)findWindowElement.Current.BoundingRectangle.Y;

            screenshot.Adornments.Add(new ClickAdornment { ClickX = screenshotX, ClickY = screenshotY });

            return true;
        }
    }
}