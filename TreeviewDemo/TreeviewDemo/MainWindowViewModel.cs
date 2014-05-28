using System;
using System.Collections.Generic;
using System.Windows;

namespace TreeviewDemo
{


    public class MainWindowViewModel
    {
        private List<IAction> actions;
    

        public MainWindowViewModel()
        {
            Text = "hello world";
            IAction action1 = new OnScreenAction
            {
                Description = "click here at this point",
                Item = "Button",
                Operation = "ClickButton",
                Value = "(2, 45)"
                
            };
            IAction action2 = new OnScreenAction
            {
                Description = "click here at this point",
                Item = "Button2",
                Operation = "ClickButton",
                Value = "(67890, 49)"
                
            };
            IList<IAction> onScreenActions2 = new List<IAction>();
            onScreenActions2.Add(action1);
            onScreenActions2.Add(action2);
            IAction action3 = new OnScreenAction
            {
                Description = "set texbox to Hello World",
                Item = "TextBox",
                Operation = "SetText",
                Value = "Hello World",
                Actions = onScreenActions2,
                HasPicture=true
                
            };
            IList<IAction> onScreenActions = new List<IAction>();
            onScreenActions.Add(action1);
            onScreenActions.Add(action2);
            onScreenActions.Add(action3);
           
            
            IAction action4 = new OnScreenAction
            {
                Description = "Select SelectedItem from ComboBox",
                Item = "ComboBox",
                Operation = "SelectComboBox",
                Value = "SelectedItem",
                Actions = onScreenActions
                
            };

            
            actions = new List<IAction>();
            actions.Add(action1);
            actions.Add(action2);
            actions.Add(action4);
            actions.Add(action3);

        }

        public List<IAction> Actions
        {
            get { return actions; }
            set { actions = value; }
        }

        public String Text { get; set; }

        public IAction SelectedAction { get; set; }

        public bool DebugState  
        {
            get { return SelectedAction.DebugState; }
            set { SelectedAction.DebugState = value; }
        }



      
    }
}