using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
        private OperationParameter imageParam { get { return Parameters[3]; } }

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

        public Point GetClickPoint(Screenshot tempScreenshot)
        {
            string text = textParam.GetValue();
            Point clickPoint = new Point(int.Parse(clickXParam.GetValue()), int.Parse(clickYParam.GetValue()));

            if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(imageParam.GetValue()))
            {
                return clickPoint;
            }

            if (!string.IsNullOrEmpty(text))
            {
                OcrAnalyzer analyzer = new OcrAnalyzer(Camera.Capture());
                OcrData deserializedData = Serializer.DeSerialize(File.ReadAllBytes(@"..\..\..\..\LearningOcr\LearningOcr\bin\Debug\TestFffff.ocr")) as OcrData;
                deserializedData.Analyze();
                IEnumerable<FoundTextData> foundTextDatas = analyzer.FindText(text, deserializedData, 0.8f);

                FoundTextData foundText = foundTextDatas.FirstOrDefault();

                if (foundText == null)
                    return clickPoint;

                return new Point(foundText.Bounds.Left + foundText.Bounds.Width / 2,
                    foundText.Bounds.Top + foundText.Bounds.Height / 2);
            }

            Bitmap bitmap = imageParam.Value as Bitmap;

            if (bitmap != null)
            {
                Rectangle rect = SearchBitmap(bitmap, tempScreenshot.Image, 0);

                return new Point(rect.Left + rect.Width/2, rect.Top + rect.Height/2);
            }


            throw new Exception("Could not get a clickpoint");
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

        protected Rectangle SearchBitmap(Bitmap smallBmp, Bitmap bigBmp, double tolerance)
        {
            BitmapData smallData =
              smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData bigData =
              bigBmp.LockBits(new Rectangle(0, 0, bigBmp.Width, bigBmp.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            int smallStride = smallData.Stride;
            int bigStride = bigData.Stride;

            int bigWidth = bigBmp.Width;
            int bigHeight = bigBmp.Height - smallBmp.Height + 1;
            int smallWidth = smallBmp.Width * 3;
            int smallHeight = smallBmp.Height;

            Rectangle location = Rectangle.Empty;
            int margin = Convert.ToInt32(255.0 * tolerance);

            unsafe
            {
                byte* pSmall = (byte*)(void*)smallData.Scan0;
                byte* pBig = (byte*)(void*)bigData.Scan0;

                int smallOffset = smallStride - smallBmp.Width * 3;
                int bigOffset = bigStride - bigBmp.Width * 3;

                bool matchFound = true;

                for (int y = 0; y < bigHeight; y++)
                {
                    for (int x = 0; x < bigWidth; x++)
                    {
                        byte* pBigBackup = pBig;
                        byte* pSmallBackup = pSmall;

                        //Look for the small picture.
                        for (int i = 0; i < smallHeight; i++)
                        {
                            int j = 0;
                            matchFound = true;
                            for (j = 0; j < smallWidth; j++)
                            {
                                //With tolerance: pSmall value should be between margins.
                                int inf = pBig[0] - margin;
                                int sup = pBig[0] + margin;
                                if (sup < pSmall[0] || inf > pSmall[0])
                                {
                                    matchFound = false;
                                    break;
                                }

                                pBig++;
                                pSmall++;
                            }

                            if (!matchFound) break;

                            //We restore the pointers.
                            pSmall = pSmallBackup;
                            pBig = pBigBackup;

                            //Next rows of the small and big pictures.
                            pSmall += smallStride * (1 + i);
                            pBig += bigStride * (1 + i);
                        }

                        //If match found, we return.
                        if (matchFound)
                        {
                            location.X = x;
                            location.Y = y;
                            location.Width = smallBmp.Width;
                            location.Height = smallBmp.Height;
                            break;
                        }
                        //If no match found, we restore the pointers and continue.
                        else
                        {
                            pBig = pBigBackup;
                            pSmall = pSmallBackup;
                            pBig += 3;
                        }
                    }

                    if (matchFound) break;

                    pBig += bigOffset;
                }
            }

            bigBmp.UnlockBits(bigData);
            smallBmp.UnlockBits(smallData);

            return location;
        }
    }
}