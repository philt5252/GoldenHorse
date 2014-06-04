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

        public override void Play(MappedItem mappedItem, Log log)
        {
            AppProcess process = AppManager.GetProcess(mappedItem);
            MappedItem window = AppManager.GetWindow(mappedItem);

            IUIItem uiItem = AppPlaybackService.GetControl(process, window, mappedItem);
            Point clickPoint = this.GetClickPoint();
            Point globalPoint = new Point((int)uiItem.Bounds.X + clickPoint.X, (int)uiItem.Bounds.Y + clickPoint.Y);
            
            Cursor.LeftClick(globalPoint);
            Bitmap bitmap = Camera.Capture();
            DateTime dateTime = DateTime.Now;
            string screenshotName = "ghscn_" + dateTime.Ticks + ".bmp";
            bitmap.Save(Path.Combine(ProjectSuiteManager.GetScreenshotsFolder(log), screenshotName));

            Screenshot screenshot = new Screenshot();

            screenshot.ImageFile = screenshotName;
            screenshot.DateTime = dateTime;

            AutomationElement findWindowElement = uiItem.AutomationElement;

            while (findWindowElement.Current.LocalizedControlType != "window")
            {
                findWindowElement = TreeWalker.ControlViewWalker.GetParent(findWindowElement);
            }

            int screenshotX = globalPoint.X - (int)findWindowElement.Current.BoundingRectangle.X;
            int screenshotY = globalPoint.Y - (int)findWindowElement.Current.BoundingRectangle.Y;

            screenshot.Adornments.Add(new ScreenshotClickAdornment { ClickX = screenshotX, ClickY = screenshotY });
            

            string description = string.Format("The {0} was clicked with the left mouse button", mappedItem.Type);
            
            log.CreateLogItem(LogItemCategory.Event, description, screenshot);
            //uiItem.Click();
        }
    }
}