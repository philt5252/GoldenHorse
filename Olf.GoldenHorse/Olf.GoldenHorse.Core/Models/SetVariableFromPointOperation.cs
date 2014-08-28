using System;
using System.Linq;
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
    public class SetVariableFromPointOperation : Operation
    {
        private OperationParameter variableParam { get { return Parameters[0]; } }
        private OperationParameter clickXParam { get { return Parameters[1]; } }
        private OperationParameter clickYParam { get { return Parameters[2]; } }

        public override string Name
        {
            get { return "Set Variable From Point"; }
        }

        public override string ParametersDescription
        {
            get { return string.Format("{0}, ({1},{2})", variableParam.Value, clickXParam.Value, clickYParam.Value); }
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

        protected override OperationParameter[] SetParameters()
        {
            var param1 = new OperationParameter
            {
                Name = "Variable",
                Mode = OperationParameterValueMode.Variable,
            };

            var param2 = new OperationParameter
            {
                Name = "ClientX",
                Mode = OperationParameterValueMode.Constant,
                Value=5
            };

            var param3 = new OperationParameter
            {
                Name = "ClientY",
                Mode = OperationParameterValueMode.Constant,
                Value=5
            };

            return new[] { param1, param2, param3 };
        }

        public override string DefaultDescription(MappedItem control)
        {
            return string.Format("Set variable {0} to value at point ({1},{2})", variableParam.Value, clickXParam.Value, clickYParam.Value);
        }

        public override bool Play(MappedItem control, Log log)
        {
            Variable variable = this.TestItem.Test.Variables.FirstOrDefault(v => v.Name.Equals(variableParam.Value));

            AppProcess process = AppManager.GetProcess(control);
            MappedItem window = AppManager.GetWindow(control);

            IUIItem uiItem = AppPlaybackService.GetControl(process, window, control, AppManager);
            Point clickPoint = new Point(X, Y);
            Point globalPoint = new Point((int)uiItem.Bounds.X + clickPoint.X, (int)uiItem.Bounds.Y + clickPoint.Y);

            Screenshot screenshot = CreateScreenshot(log);

            Cursor.LeftClick(globalPoint);
            string actualText = CopyText();

            variable.Value = actualText;

            log.CreateLogItem(LogItemCategory.Message, string.Format("Variable '{0}' was set to value: {1}", variable.Name, variable.Value));

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