using System;
using System.Drawing;
using System.Linq;

namespace LearningOcr.Core
{
    public class BitmapFinder
    {
        private float colorTolerance = 0.1f;
        private float accuracyTolerance = 0.01f;
        private LockBitmap lockSourceBitmap;
        private LockBitmap lockBitmapToFind;

        public BitmapFinder(Bitmap sourceBitmap, Bitmap bitmapToFind)
        {
            lockSourceBitmap = new LockBitmap(sourceBitmap);
            lockBitmapToFind = new LockBitmap(bitmapToFind);
        }

        public Point Find()
        {
            lockSourceBitmap.LockBits();
            lockBitmapToFind.LockBits();

            Point retPoint = new Point(0, 0);
            bool found = false;

            for (int sourceY = 0; sourceY < lockSourceBitmap.Height && !found; sourceY++)
            {
                for (int sourceX = 0; sourceX < lockSourceBitmap.Width && !found; sourceX++)
                {
                    if (CheckForBitmap(sourceX, sourceY))
                    {
                        found = true;
                        retPoint = new Point(sourceX, sourceY);
                    }
                }
            }

            lockSourceBitmap.UnlockBits();
            lockBitmapToFind.UnlockBits();

            if (!found)
                throw new Exception("Bitmap not found");


            return retPoint;
        }

        private bool CheckForBitmap(int sourceX, int sourceY)
        {
            if (sourceX + lockBitmapToFind.Width > lockSourceBitmap.Width
                || sourceY + lockBitmapToFind.Height > lockSourceBitmap.Height)
                return false;

            int errors = 0;
            int maxErrors = (int)(lockBitmapToFind.Pixels.Length * accuracyTolerance);

            for (int findY = 0; findY < lockBitmapToFind.Height; findY++)
            {
                for (int findX = 0; findX < lockBitmapToFind.Width; findX++)
                {
                    Color sourcePixel = lockSourceBitmap.GetPixel(sourceX + findX, sourceY + findY);
                    Color findPixel = lockBitmapToFind.GetPixel(findX, findY);

                    if (!(Math.Abs(sourcePixel.A - findPixel.A) <= colorTolerance * 255
                        && Math.Abs(sourcePixel.R - findPixel.R) <= colorTolerance * 255
                        && Math.Abs(sourcePixel.G - findPixel.G) <= colorTolerance * 255
                        && Math.Abs(sourcePixel.B - findPixel.B) <= colorTolerance * 255))
                    {
                        errors++;
                        if (errors > maxErrors)
                            return false;
                    }
                }
            }

            return true;
        }
    }
}