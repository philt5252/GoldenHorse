
using System;
using System.ComponentModel;
using System.Linq;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class LogMainShellViewModel : ILogMainShellViewModel
    {
        private readonly Log log;
        public ILogScreenshotsViewModel LogScreenshotsViewModel { get; protected set; }
        public ILogShellViewModel LogShellViewModel { get; protected set; }
        public string LogName { get { return log.Name; } }

        public LogMainShellViewModel(Log log, ILogShellViewModelFactory logShellViewModelFactory,
            ILogScreenshotsViewModelFactory logScreenshotsViewModelFactory)
        {
            this.log = log;
            LogShellViewModel = logShellViewModelFactory.Create(log);
            LogScreenshotsViewModel = logScreenshotsViewModelFactory.Create(log);

            LogShellViewModel.LogDetailsViewModel.PropertyChanged += LogDetailsViewModelOnPropertyChanged;
            LogScreenshotsViewModel.PropertyChanged += LogScreenshotsViewModelOnPropertyChanged;

        }

        private void LogScreenshotsViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "SelectedScreenshot")
            {
                LogShellViewModel.LogDetailsViewModel.SelectedLogItem =
                    LogShellViewModel.LogDetailsViewModel.LogItems.FirstOrDefault(
                        t => t.LogItem == LogScreenshotsViewModel.SelectedScreenshot.Owner);

            }
        }

        private void LogDetailsViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "SelectedLogItem")
            {
                if (LogShellViewModel.LogDetailsViewModel.SelectedLogItem == null)
                    return;

                if (!LogShellViewModel.LogDetailsViewModel.SelectedLogItem.HasScreenshot)
                    return;

                LogScreenshotsViewModel.SelectedScreenshot =
                    LogScreenshotsViewModel.Screenshots.FirstOrDefault(
                        s => s.Owner == LogShellViewModel.LogDetailsViewModel.SelectedLogItem.LogItem);
            }
        }
    }
}