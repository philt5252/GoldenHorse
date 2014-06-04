using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class LogNode : DisplayNode
    {
        private readonly Log log;

        public override string Name
        {
            get { return log.Name; }
        }

        public LogNode(Log log)
        {
            this.log = log;
        }
    }
}