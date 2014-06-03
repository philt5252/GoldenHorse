using System.Drawing;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using TestStack.White.UIItems;

namespace Olf.GoldenHorse.Core.Models
{
    public class RightClickOperation : ClickOperation
    {
        public override string Name
        {
            get { return "Right Click"; }
        }

        public override void Play(MappedItem mappedItem)
        {
            //IUIItem uiItem = AppPlaybackService.GetControl(processName, windowName, controlName);

            

            //uiItem.RightClick();
        }
    }
}