using System.Collections.Generic;

namespace TestForGolden
{
    public class OnScreenAction : TestItem
    {
        public string WindowId { get; set; }
        public string ControlId { get; set; }
        public string Description { get; set; }
    }
}