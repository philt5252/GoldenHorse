using System;
using System.Data;
using System.IO;
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
    public class SetTableVariableFromFileOperation : Operation
    {
        private OperationParameter variableParam { get { return Parameters[0]; } }
        private OperationParameter filePathParam { get { return Parameters[1]; } }

        public override string Name
        {
            get { return "Set Table Variable From File"; }
        }

        public override string ParametersDescription
        {
            get { return string.Format("Set {0} from {1}", variableParam.Value, filePathParam.Value); }
        }

        public string FilePath
        {
            get { return ReferenceEquals(filePathParam.Value, "") ? "<Provide a file path>" : filePathParam.Value.ToString(); }
            set
            {
                filePathParam.Value = value;
                filePathParam.Mode = OperationParameterValueMode.Constant;
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
                Name = "File Path",
                Mode = OperationParameterValueMode.Constant,
                Value = ""
            };

            return new[] { param1, param2 };
        }

        public override string DefaultDescription(MappedItem control)
        {
            return string.Format("Set table variable {0} from file {1}", variableParam.Value, filePathParam.Value);
        }

        public override bool Play(MappedItem control, Log log)
        {
            Variable variable = this.TestItem.Test.Variables.FirstOrDefault(v => v.Name.Equals(variableParam.Value));
            
            string filePath = filePathParam.GetValue();

            string[] fileLines = File.ReadAllLines(filePath);

            DataTable dataTable = variable.DataTableValue;

            string[] columnNames = variable.DataTableValue.Columns.OfType<DataColumn>().Select(c => c.ColumnName).ToArray();

            dataTable.Clear();

            foreach (string row in fileLines)
            {
                string[] fields = row.Split(',');
                dataTable.Rows.Add(fields);
            }

            log.CreateLogItem(LogItemCategory.Message, string.Format("Variable '{0}' was set from file : {1}", variable.Name, filePath));

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