using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class OnScreenAction : TestItem
    {
        public string WindowId { get; set; }
        public string ControlId { get; set; }
        public Operation Operation { get; set; }
    }
}