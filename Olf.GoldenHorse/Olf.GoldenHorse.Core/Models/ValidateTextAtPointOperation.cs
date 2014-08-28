using System;
using System.Threading;
using System.Windows;
using WindowsInput;
using WindowsInput.Native;
using Olf.Automation;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using TestStack.White.UIItems;
using Point = System.Drawing.Point;

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
            get { return ReferenceEquals(clickXParam.Value, "") ? 0 : int.Parse(clickXParam.Value.ToString()); }
            set
            {
                clickXParam.Value = value;
                clickXParam.Mode = OperationParameterValueMode.Constant;
            }
        }

        public int Y
        {
            get { return ReferenceEquals(clickXParam.Value, "") ? 0 : int.Parse(clickYParam.Value.ToString()); }
            set
            {
                clickYParam.Value = value;
                clickYParam.Mode = OperationParameterValueMode.Constant;
            }
        }

        public override string ParametersDescription
        {
            get { return textParam.Value == null ? "" : textParam.Value.ToString(); }
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
            string controlName = control == null ? "<Unset Control>" : control.Name;

            return string.Format(description, X, Y, controlName, Text);
        }

        public override bool Play(MappedItem control, Log log)
        {
            AppProcess process = AppManager.GetProcess(control);
            MappedItem window = AppManager.GetWindow(control);

            IUIItem uiItem = AppPlaybackService.GetControl(process, window, control, AppManager);
            Point clickPoint = new Point(X,Y);
            Point globalPoint = new Point((int)uiItem.Bounds.X + clickPoint.X, (int)uiItem.Bounds.Y + clickPoint.Y);
            
            Screenshot screenshot = CreateScreenshot(log);

            Cursor.LeftClick(globalPoint);
            string actualText = CopyText();
            string expectedText = textParam.GetValue();

            if (actualText == null)
                actualText = "";

            if (expectedText == null)
                expectedText = "";

            if(!Equals(actualText, expectedText))
            {
                string error = "Expected \"{0}\" but value was \"{1}\"";
                error = string.Format(error, expectedText, actualText);

                log.CreateLogItem(LogItemCategory.Error, error, screenshot);
                return false;
            }

            string description = "Validated that the text at ({0},{1}) on {2} is equal to \"{3}\"";
            description = string.Format(description, X, Y, control.Name, Text);
            
            log.CreateLogItem(LogItemCategory.Validation, description, screenshot);

            return true;
        }

        private string CopyText()
        {
            IDataObject oldDataObject = Clipboard.GetDataObject();

            Clipboard.Clear();

            Thread.Sleep(500);
            InputSimulator simulator = new InputSimulator();
            simulator.Keyboard.KeyDown(VirtualKeyCode.HOME);

            Thread.Sleep(200);
            simulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
            simulator.Keyboard.KeyDown(VirtualKeyCode.END);
            simulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);

            Thread.Sleep(100);
            simulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
            simulator.Keyboard.KeyDown(VirtualKeyCode.VK_C);
            simulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);


            Thread.Sleep(100);
            int count = 0;
            string clipboardText = "{Clipboard Text could not be retreived}";
            int milliseconds = 200;
            DateTime dateTime = DateTime.Now;
            do
            {
                if (DateTime.Now - dateTime > TimeSpan.FromSeconds(20))
                {
                    throw new Exception("Could not access the clipboard");
                }
                try
                {
                    IDataObject dataObject = Clipboard.GetDataObject();
                    object data = dataObject.GetData(typeof(string));
                    
                    if (data == null)
                        data = "";
                    
                    string copyText = clipboardText = data.ToString();
                    Clipboard.SetDataObject(oldDataObject);
                    return copyText;
                }
                catch (Exception ex)
                {
                    //count++;
                    Thread.Sleep(milliseconds);
                    milliseconds += 100;
                }


            } while (clipboardText.Equals("{Clipboard Text could not be retreived}"));

            Clipboard.SetDataObject(oldDataObject);

            return clipboardText;
        }
    }
}