﻿using System.Drawing;
using Olf.Automation;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using TestStack.White.UIItems;

namespace Olf.GoldenHorse.Core.Models
{
    public class ValidateTextAtPointOperation : Operation
    {
        private OperationParameter textParam { get { return Parameters[0]; } }
        private OperationParameter clickXParam { get { return Parameters[1]; } }
        private OperationParameter clickYParam { get { return Parameters[2]; } }


        public override string Name
        {
            get { return "Validate text at point"; }
        }

        public string Text
        {
            get { return (string)textParam.Value; }
            set
            {
                textParam.Value = value;
                textParam.Mode = OperationParameterValueMode.Constant;
            }
        }

        public int X
        {
            get { return int.Parse(clickXParam.Value.ToString()); }
            set
            {
                clickXParam.Value = value;
                textParam.Mode = OperationParameterValueMode.Constant;
            }
        }

        public int Y
        {
            get { return int.Parse(clickYParam.Value.ToString()); }
            set
            {
                clickYParam.Value = value;
                textParam.Mode = OperationParameterValueMode.Constant;
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
                Name = "Text",
                Mode = OperationParameterValueMode.Constant
            };

            var param2 = new OperationParameter
            {
                Name = "ClientX",
                Mode = OperationParameterValueMode.Constant
            };

            var param3 = new OperationParameter
            {
                Name = "ClientY",
                Mode = OperationParameterValueMode.Constant
            };

            return new[] { param1, param2, param3 };
        }

        public override string DefaultDescription(MappedItem control)
        {
            string description = "Validates that the text at ({0},{1}) on {2} is equal to \"{3}\"";
            
            return string.Format(description, X, Y, control.Name, Text);
        }

        public override void Play(MappedItem control, Log log)
        {
            AppProcess process = AppManager.GetProcess(control);
            MappedItem window = AppManager.GetWindow(control);

            IUIItem uiItem = AppPlaybackService.GetControl(process, window, control);
            Point clickPoint = new Point(X,Y);
            Point globalPoint = new Point((int)uiItem.Bounds.X + clickPoint.X, (int)uiItem.Bounds.Y + clickPoint.Y);

            Cursor.LeftClick(globalPoint);
            Screenshot screenshot = CreateScreenshot(log);

            string description = "Validated that the text at ({0},{1}) on {2} is equal to \"{3}\"";
            description = string.Format(description, X, Y, control.Name, Text);
            

            log.CreateLogItem(LogItemCategory.Validation, description, screenshot);
        }
    }
}