using System.Collections.Generic;

namespace Olf.GoldenHorse.Foundation.Models
{
    public abstract class Operation
    {
        public abstract string Name { get; }

        public List<OperationParameter> Parameters { get; set; }

        protected Operation()
        {
            Parameters = new List<OperationParameter>();
        }
    }
}