using System;
using System.ComponentModel;
using LearningOcr.Core.Annotations;

namespace LearningOcr.Core
{
    [Serializable]
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}