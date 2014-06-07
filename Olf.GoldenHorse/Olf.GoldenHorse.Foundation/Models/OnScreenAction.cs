using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class OnScreenAction : TestItem
    {
        private string description;

        private Operation operation;
        public Operation Operation
        {
            get { return operation; }
            set
            {
                operation = value;
                operation.TestItem = this;
            }
        }

        
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
            return Operation.DefaultDescription(Control);
        }

        public override void Play(Log log)
        {
            Operation.Play(Control, log);
        }
    }
}