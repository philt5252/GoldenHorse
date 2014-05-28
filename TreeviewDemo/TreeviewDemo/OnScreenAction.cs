using System.Collections.Generic;

namespace TreeviewDemo
{
    public class OnScreenAction : IAction
    {
        private string item;
        private bool debugState;

        public string Item
        {
            get { return item; }
            set { item = value; }
        }

        public string Operation { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public IList<IAction> Actions { get; set; }

        public bool DebugState
        {
            get { return debugState; }
            set { debugState = value; }
        }

        public bool HasPicture { get; set; }
    }
}