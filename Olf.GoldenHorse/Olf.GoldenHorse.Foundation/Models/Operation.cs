using System.Collections.Generic;

namespace Olf.GoldenHorse.Foundation.Models
{
    public abstract class Operation
    {
        public abstract string Name { get; }

        public OperationParameter[] Parameters { get; set; }

        protected Operation()
        {
            SetParameters();
        }

        protected abstract OperationParameter[] SetParameters();
    }
}