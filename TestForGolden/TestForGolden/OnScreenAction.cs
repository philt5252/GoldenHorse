using System.Collections.Generic;
using System.Windows.Controls.Primitives;

namespace TestForGolden
{
    public class OnScreenAction : TestItem
    {
        public string WindowId { get; set; }
        public string ControlId { get; set; }
        public Operation Operation { get; set; }
    }
}