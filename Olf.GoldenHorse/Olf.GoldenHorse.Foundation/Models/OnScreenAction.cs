using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class OnScreenAction : TestItem
    {
        private string description;
        private MappedItem control;
        private Operation operation;
        public string ControlId { get; set; }

        public Operation Operation
        {
            get { return operation; }
            set
            {
                operation = value;
                operation.TestItem = this;
            }
        }

        [XmlIgnore]
        public MappedItem Control { get { return control ?? (control = AppManager.GetMappedItem(ControlId)); } }

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