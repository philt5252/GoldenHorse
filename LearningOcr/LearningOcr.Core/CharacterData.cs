using System;
using System.Drawing;

namespace LearningOcr.Core
{
    [Serializable]
    public class CharacterData : NotifyPropertyChangedBase
    {
        private char letter;
        private Bitmap image;
        private bool[,] letterData;

        public NeighborDifference[,] Difference;

        public int WritingLinePosition { get; set; }

        public int HeightFromWritingLine
        {
            get
            {
                return Height - (Height - WritingLinePosition);
            }
        }

        public char Letter
        {
            get { return letter; }
            protected set { letter = value; }
        }

        public Bitmap Image
        {
            get { return image; }
            set
            {
                if (Equals(image, value))
                    return;

                image = value;
                OnPropertyChanged("Image");
            }
        }

        public int Width
        {
            get
            {
                return Image.Width;
            }
        }

        public int Height
        {
            get
            {
                return Image.Height;
            }
        }

        public CharacterData(char letter, Bitmap image)
        {
            Image = image;
            Letter = letter;
            WritingLinePosition = Image.Height - 1;
            Difference = new NeighborDifference[Image.Height, Image.Width];
            InitializeLetterData();
        }

        private void InitializeLetterData()
        {
            letterData = new bool[Image.Height, Image.Width];

            for (int y = 0; y < Image.Height; y++)
            {
                for (int x = 0; x < Image.Width; x++)
                {
                    letterData[y, x] = true;
                }
            }
        }

        public void SetPixelAsCharacterData(int x, int y, bool isPartOfChar)
        {
            letterData[y, x] = isPartOfChar;
        }

        public bool IsPixelCharData(int x, int y)
        {
            return letterData[y, x];
        }

        public void Analyze()
        {
            if(Difference == null)
                Difference =  new NeighborDifference[Image.Height, Image.Width];

            LockBitmap lockBitmap = new LockBitmap(Image);
            lockBitmap.LockBits();

            for (int y = 0; y < lockBitmap.Height; y++)
            {
                for (int x = 0; x < lockBitmap.Width; x++)
                {
                    int up = y - 1;
                    int down = y + 1;
                    int left = x - 1;
                    int right = x + 1;

                    Color pixel = lockBitmap.GetPixel(x, y);

                    NeighborDifference neighborDifference = new NeighborDifference();
                    bool isCharData = IsPixelCharData(x, y);
                    neighborDifference.IsPartOfChar = isCharData;
                    
                    if (up < 0 /*|| IsPixelCharData(x, up) != isCharData*/)
                    {
                        neighborDifference.HasUp = false;
                    }
                    else
                    {
                        neighborDifference.HasUp = IsPixelCharData(x, up) == isCharData;

                        Color upPixel = lockBitmap.GetPixel(x, up);

                        neighborDifference.Up = GetPixelDistance(pixel, upPixel);
                    }

                    if (down >= lockBitmap.Height/*lockBitmap.Height || IsPixelCharData(x, down) != isCharData*/)
                    {
                        neighborDifference.HasDown = false;
                    }
                    else
                    {
                        neighborDifference.HasDown = IsPixelCharData(x, down) == isCharData;
                    
                        Color downPixel = lockBitmap.GetPixel(x, down);

                        neighborDifference.Down = GetPixelDistance(pixel, downPixel);
                    }

                    if (left < 0 /*|| IsPixelCharData(left, y) != isCharData*/)
                    {
                        neighborDifference.HasLeft = false;
                    }
                    else
                    {
                        neighborDifference.HasLeft = IsPixelCharData(left, y) == isCharData;
                    
                        Color leftPixel = lockBitmap.GetPixel(left, y);

                        neighborDifference.Left = GetPixelDistance(pixel, leftPixel);
                    }

                    if (right >= lockBitmap.Width/* || IsPixelCharData(right, y) != isCharData*/)
                    {
                        neighborDifference.HasRight = false;
                    }
                    else
                    {
                        neighborDifference.HasRight = IsPixelCharData(right, y) == isCharData;
                    
                        Color rightPixel = lockBitmap.GetPixel(right, y);

                        neighborDifference.Right = GetPixelDistance(pixel, rightPixel);
                    }

                    Difference[y, x] = neighborDifference;
                }
            }

            lockBitmap.UnlockBits();
        }

        private static float maxDist = (float)Math.Sqrt(Math.Pow(255, 2) * 3);
        private static float GetPixelDistance(Color color1, Color color2)
        {
            return (float)Math.Sqrt(
                Math.Pow(color1.R - color2.R, 2)
                + Math.Pow(color1.G - color2.G, 2)
                + Math.Pow(color1.B - color2.B, 2))/maxDist;
        }
    }
}