using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class OnScreenAction : TestItem
    {
        private string description;
        public string ProcessName { get; set; }
        public string WindowName { get; set; }
        public string ControlName { get; set; }
        public Operation Operation { get; set; }

        public override string Description
        {
            get { return description ?? DefaultDescription(); }
            set { description = value; }
        }

        public OnScreenAction()
        {
            
        }

        protected virtual string DefaultDescription()
        {
            return Operation.DefaultDescription(WindowName, ControlName);
        }

        public override void Play()
        {
            Operation.Play(ProcessName, WindowName, ControlName);
        }
    }
}