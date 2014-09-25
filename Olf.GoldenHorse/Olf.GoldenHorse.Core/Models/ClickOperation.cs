using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using LearningOcr.Core;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using TestStack.White.UIItems;

namespace Olf.GoldenHorse.Core.Models
{
    public abstract class ClickOperation : Operation
    {
        private OperationParameter clickXParam { get { return Parameters[0]; } }
        private OperationParameter clickYParam { get { return Parameters[1]; } }
        private OperationParameter textParam { get { return Parameters[2]; } }
        private OperationParameter imageParam { get { return Parameters[2]; } }

        public override string ParametersDescription
        {
            get { return string.Format("{0}, {1}", clickXParam.Value, clickYParam.Value); }
        }

        protected ClickOperation()
        {
            
        }

        public void SetClickPoint(int x, int y)
        {
            clickXParam.Value = x;
            clickYParam.Value = y;
        }

        public Point GetClickPoint()
        {
            string text = textParam.GetValue();
            Point clickPoint = new Point(int.Parse(clickXParam.GetValue()), int.Parse(clickYParam.GetValue()));

            if(string.IsNullOrEmpty(text))
            {
                return clickPoint;
            }

            OcrAnalyzer analyzer = new OcrAnalyzer(Camera.Capture());
            OcrData deserializedData = Serializer.DeSerialize(File.ReadAllBytes(@"..\..\..\..\LearningOcr\LearningOcr\bin\Debug\TestFffff.ocr")) as OcrData;
            deserializedData.Analyze();
            IEnumerable<FoundTextData> foundTextDatas = analyzer.FindText(text, deserializedData, 0.8f);

            FoundTextData foundText = foundTextDatas.FirstOrDefault();

            if (foundText == null)
                return clickPoint;

            return new Point(foundText.Bounds.Left + foundText.Bounds.Width/2,
                foundText.Bounds.Top + foundText.Bounds.Height/2);
        }

        public override string DefaultDescription(MappedItem control)
        {
            return string.Format("Click on {0} at ({1})", control.FriendlyName, ParametersDescription);
        }

        protected override OperationParameter[] SetParameters()
        {
            var param1 = new OperationParameter
            {
                Name = "ClientX",
                Mode = OperationParameterValueMode.Constant
            };

            var param2 = new OperationParameter
            {
                Name = "ClientY",
                Mode = OperationParameterValueMode.Constant
            };

            var param3 = new OperationParameter
            {
                Name = "Text",
                Mode = OperationParameterValueMode.Constant
            };

            var param4 = new OperationParameter
            {
                Name = "Image",
                TypeIdentifier="Image",
                Mode = OperationParameterValueMode.Constant
            };

            return new[] { param1, param2, param3, param4 };
        }
    }
}