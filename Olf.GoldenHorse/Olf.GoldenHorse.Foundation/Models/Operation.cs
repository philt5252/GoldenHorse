using System.Collections.Generic;

namespace Olf.GoldenHorse.Foundation.Models
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