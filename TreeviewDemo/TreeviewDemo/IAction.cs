using System.Collections;
using System.Collections.Generic;

namespace TreeviewDemo
{
    public interface IAction
    {
        string Item { get; set; } 
        string Operation { get; set; }
        string Value { get; set; }
        string Description { get; set; }
        IList<IAction> Actions { get; set; }
        bool DebugState { get; set; }
        bool HasPicture { get; set; }

    }
}