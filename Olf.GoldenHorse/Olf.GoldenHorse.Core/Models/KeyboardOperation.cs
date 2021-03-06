﻿using System.Net.Mime;
using System.Security.Cryptography;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Xml.Serialization;
using Olf.Automation;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using TestStack.White.UIItems;

namespace Olf.GoldenHorse.Core.Models
{
    public class KeyboardOperation : Operation
    {
        private OperationParameter textParam{ get { return Parameters[0]; } }

        public override string Name
        {
            get { return "Keyboard"; }
        }

        [XmlIgnore]
        public string Text
        {
            get { return (string)textParam.Value; }
            set
            {
                textParam.Value = value;
            }
        }

        public override string ParametersDescription
        {
            get { return textParam.Value.ToString(); }
        }

        protected override OperationParameter[] SetParameters()
        {
            var param1 = new OperationParameter
            {
                Name="Text",
                Mode = OperationParameterValueMode.Constant
            };

            return new[] { param1 };
        }

        public override string DefaultDescription(MappedItem mappedItem)
        {
            if(mappedItem == null)
                return string.Format("Enters \"{0}\"", textParam.Value);

            return string.Format("Enters \"{0}\" in the '{1}' object", textParam.Value, mappedItem.FriendlyName);
        }

        public override bool Play(MappedItem mappedItem, Log log)
        {
            string text = textParam.GetValue();
            Keyboard.SendKeys(text);
            

            AppProcess process = AppManager.GetProcess(mappedItem);
            MappedItem window = AppManager.GetWindow(mappedItem);

            IUIItem uiItem = AppPlaybackService.GetControl(process, window, mappedItem, AppManager);

            AutomationElement findWindowElement = uiItem.AutomationElement;

            while (findWindowElement.Current.LocalizedControlType != "window"
                && !(findWindowElement.Current.LocalizedControlType == "pane" 
                 && TreeWalker.ControlViewWalker.GetParent(findWindowElement).Current.LocalizedControlType == "process"))
            {
                findWindowElement = TreeWalker.ControlViewWalker.GetParent(findWindowElement);
            }

            Screenshot screenshot = CreateScreenshot(log, findWindowElement.Current.NativeWindowHandle);

            screenshot.Adornments.Add(
                new ControlHighlightAdornment
                {
                    X = (int)uiItem.Bounds.X - (int)findWindowElement.Current.BoundingRectangle.X,
                    Y = (int)uiItem.Bounds.Y - (int)findWindowElement.Current.BoundingRectangle.Y,
                    Width = (int)uiItem.Bounds.Width,
                    Height = (int)uiItem.Bounds.Height
                });


            string description = string.Format("The text '{0}' was entered in {1}", text, mappedItem.Name);
            log.CreateLogItem(LogItemCategory.Event, description, screenshot);

            return true;
        }  
    }
}