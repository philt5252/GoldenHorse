using System.Collections.Generic;
using System.Windows.Documents;

namespace TestForGolden
{
    public abstract class Operation
    {
        private readonly string name;

        public virtual string Name
        {
            get { return name; }
        }

        public List<OperationParameter> Parameters { get; set; }

        protected Operation()
        {
            name = this.GetType().Name.Replace("Operation", "");
            Parameters = new List<OperationParameter>();
        }
    }
}