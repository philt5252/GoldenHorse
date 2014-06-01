using System.Collections.Generic;

namespace Olf.GoldenHorse.Foundation.Models
{
    public abstract class Operation
    {
        public abstract string Name { get; }
        public abstract string ParametersDescription { get; }

        public OperationParameter[] Parameters { get; set; }

        protected Operation()
        {
            Parameters = SetParameters();
        }

        protected abstract OperationParameter[] SetParameters();

        public abstract string DefaultDescription(string windowId, string controlId);

        public abstract void Play(string processName, string windowName, string controlName);
    }
}