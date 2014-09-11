using System;
using System.Drawing;
using Microsoft.Win32;

namespace LearningOcr.Core
{
    public class OcrCharacterAnalyzer
    {
        public NeighborDifference[,] GetNeighborDifferences(Bitmap bitmap)
        {
            LockBitmap lockBitmap = new LockBitmap(bitmap);
            lockBitmap.LockBits();

            NeighborDifference[,] retDifference = new NeighborDifference[bitmap.Height, bitmap.Width];
            Color[,] colors = new Color[lockBitmap.Height, lockBitmap.Width];

            for (int y = 0; y < lockBitmap.Height; y++)
            {
                for (int x = 0; x < lockBitmap.Width; x++)
                {
                    colors[y, x] = lockBitmap.GetPixel(x, y);
                }
            }

            for (int y = 0; y < lockBitmap.Height; y++)
            {
                for (int x = 0; x < lockBitmap.Width; x++)
                {
                    int up = y - 1;
                    int down = y + 1;
                    int left = x - 1;
                    int right = x + 1;

                    Color pixel = colors[y, x];

                    NeighborDifference neighborDifference = new NeighborDifference();
                    
                    if (up < 0)
                    {
                        neighborDifference.HasUp = false;
                    }
                    else if (up > 0)
                    {
                        neighborDifference.Up = retDifference[up, x].Down;
                    }
                    else
                    {
                        Color upPixel = colors[up, x];

                        neighborDifference.Up = GetPixelDistance(pixel, upPixel);
                    }

                    if (down >= lockBitmap.Height)
                    {
                        neighborDifference.HasDown = false;
                    }
                    else
                    {
                        Color downPixel = colors[down, x];

                        neighborDifference.Down = GetPixelDistance(pixel, downPixel);
                    }

                    if (left < 0)
                    {
                        neighborDifference.HasLeft = false;
                    }
                    else if (left > 0)
                    {
                        neighborDifference.Left = retDifference[y, left].Right;
                    }
                    else
                    {
                        Color leftPixel = colors[y, left];

                        neighborDifference.Left = GetPixelDistance(pixel, leftPixel);
                    }

                    if (right >= lockBitmap.Width)
                    {
                        neighborDifference.HasRight = false;
                    }
                    else
                    {
                        Color rightPixel = colors[y, right];

                        neighborDifference.Right = GetPixelDistance(pixel, rightPixel);
                    }

                    retDifference[y, x] = neighborDifference;
                }
            }

            lockBitmap.UnlockBits();

            return retDifference;
        }


        private static float maxDist = (float)Math.Sqrt(Math.Pow(255, 2)*3);
        private static float GetPixelDistance(Color color1, Color color2)
        {
            int diffR = color1.R - color2.R;
            int diffG = color1.G - color2.G;
            int diffB = color1.B - color2.B;
            return (float) Math.Sqrt(
                diffR * diffR
                + diffG * diffG
                + diffB * diffB)/maxDist;
        }
    }
}