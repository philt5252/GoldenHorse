
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class LogDetailsViewModel : ViewModelBase, ILogDetailsViewModel
    {
        private readonly Log log;
        private readonly ILogItemViewModelFactory logItemViewModelFactory;
        private ILogItemViewModel selectedLogItem;
        private bool showErrors;
        private bool showWarnings;
        private bool showEvents;
        private bool showMessages;
        private bool showCheckpoints;
        private ObservableCollection<ILogItemViewModel> logItems;

        public ObservableCollection<ILogItemViewModel> LogItems
        {
            get { return logItems; }
            set
            {
                logItems = value;
                OnPropertyChanged("LogItems");
            }
        }

        public bool ShowErrors
        {
            get { return showErrors; }
            set
            {
                showErrors = value;
                RefreshLogItems();
            }
        }

        public bool ShowWarnings
        {
            get { return showWarnings; }
            set
            {
                showWarnings = value;
                RefreshLogItems();
            }
        }

        public bool ShowEvents
        {
            get { return showEvents; }
            set
            {
                showEvents = value;
                RefreshLogItems();
            }
        }

        public bool ShowMessages
        {
            get { return showMessages; }
            set
            {
                showMessages = value;
                RefreshLogItems();
            }
        }

        public bool ShowCheckpoints
        {
            get { return showCheckpoints; }
            set
            {
                showCheckpoints = value;
                RefreshLogItems();
            }
        }

        public ILogItemViewModel SelectedLogItem
        {
            get { return selectedLogItem; }
            set
            {
                if (Equals(selectedLogItem, value))
                    return;

                selectedLogItem = value;

                OnPropertyChanged("SelectedLogItem");
            }
        }

        public LogDetailsViewModel(Log log, ILogItemViewModelFactory logItemViewModelFactory)
        {
            this.log = log;
            this.logItemViewModelFactory = logItemViewModelFactory;

            showCheckpoints = true;
            showErrors = true;
            showEvents = true;
            showMessages = true;
            showWarnings = true;

            RefreshLogItems();
        }

        private void RefreshLogItems()
        {
            LogItems = new ObservableCollection<ILogItemViewModel>(
                log.LogItems
                .Where(l =>
                {
                    if (ShowErrors && l.Category == LogItemCategory.Error)
                        return true;
                    if (ShowCheckpoints && l.Category == LogItemCategory.Checkpoint)
                        return true;
                    if (ShowEvents && l.Category == LogItemCategory.Event)
                        return true;
                    if (ShowMessages && l.Category == LogItemCategory.Message)
                        return true;
                    if (ShowWarnings && l.Category == LogItemCategory.Warning)
                        return true;

                    return false;
                }).Select(logItemViewModelFactory.Create));
        }
    }
}