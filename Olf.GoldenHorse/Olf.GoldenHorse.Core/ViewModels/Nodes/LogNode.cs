using System.Drawing;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.ViewModels.Nodes
{
    public class LogNode : DisplayNode
    {
        private readonly ProjectFile logFile;
        private readonly ILogFileManager logFileManager;
        private readonly ILogController logController;
        private Log log;

        public override bool IsRenamable
        {
            get { return true; }
        }

        public override string Name
        {
            get { return logFile.Name.Replace(DefaultData.LogExtension, ""); }
            set
            {
                if (Equals(logFile.Name.Replace(DefaultData.LogExtension, ""), value))
                    return;

                logFileManager.Rename(logFile, value);

                log.Name = value;
            }
        }

        public LogNode(ProjectFile logFile, ILogFileManager logFileManager,
            ILogController logController)
        {
            this.logFile = logFile;
            this.logFileManager = logFileManager;
            this.logController = logController;

            DefaultCommand = new DelegateCommand(ExecuteDefaultCommand);
        }

        protected virtual void ExecuteDefaultCommand()
        {
            log = logFileManager.Open(logFile.FilePath);
            log.Owner = logFile.Project;
            logController.ShowLog(log);
        }
    }
}