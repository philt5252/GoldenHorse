using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Text;

namespace LearningOcr.Core
{
    public class OcrAnalyzer
    {
        private readonly Bitmap bitmap;
        private NeighborDifference[,] cahcedNeighborDifferences;

        public OcrAnalyzer(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        private NeighborDifference[,] neighborDifferences
        {
            get
            {
                if (cahcedNeighborDifferences != null)
                    return cahcedNeighborDifferences;

                OcrCharacterAnalyzer ocrCharacterAnalyzer = new OcrCharacterAnalyzer();

                cahcedNeighborDifferences = ocrCharacterAnalyzer.GetNeighborDifferences(bitmap);
                return cahcedNeighborDifferences;
            }
        }

        public IEnumerable<FoundTextData> FindCharacterData(CharacterData characterData, float tolerance = 0.05f)
        {
            for (int y = 0; y < neighborDifferences.GetLength(0); y++)
            {
                for (int x = 0; x < neighborDifferences.GetLength(1); x++)
                {
                    if (IsCharacterMatchAtPoint(x, y, neighborDifferences, characterData, tolerance))
                    {
                        yield return
                            new FoundTextData
                            {
                                Bounds = new Rectangle(x, y, characterData.Width, characterData.Height),
                                Text = characterData.Letter.ToString()
                            };
                    }
                }
            }
        }


        private bool IsCharacterMatchAtPoint(int x, int y,
                NeighborDifference[,] searchDifferences, CharacterData characterData,
                float tolerance)
        {
            if (x + characterData.Difference.GetLength(1) >= searchDifferences.GetLength(1)
                || y + characterData.Difference.GetLength(0) >= searchDifferences.GetLength(0))
                return false;

            if (x == 31 && y == 140)
            {

            }

            for (int charY = 0; charY < characterData.Difference.GetLength(0); charY++)
            {
                for (int charX = 0; charX < characterData.Difference.GetLength(1); charX++)
                {
                    if (x + charX == 279 && y + charY == 192)
                    {

                    }

                    NeighborDifference characterDifference = characterData.Difference[charY, charX];
                    NeighborDifference searchDifference = searchDifferences[y + charY, x + charX];
                    if ((characterDifference.HasUp
                         && Math.Abs(characterDifference.Up - searchDifference.Up)
                         > tolerance)
                        || (characterDifference.HasDown
                         && Math.Abs(characterDifference.Down - searchDifference.Down)
                         > tolerance)
                        || (characterDifference.HasLeft
                         && Math.Abs(characterDifference.Left - searchDifference.Left)
                         > tolerance)
                        || (characterDifference.HasRight
                         && Math.Abs(characterDifference.Right - searchDifference.Right)
                         > tolerance))
                    {
                        return false;
                    }

                    
                    
                    if (characterDifference.IsPartOfChar
                        &&((!characterDifference.HasUp
                         && Math.Abs(searchDifference.Up) < 0.05)
                        || (!characterDifference.HasDown
                         && Math.Abs(searchDifference.Down) < 0.05)
                        || (!characterDifference.HasLeft
                         && Math.Abs(searchDifference.Left) < 0.05)
                        || (!characterDifference.HasRight
                         && Math.Abs(searchDifference.Right) < 0.05)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private FoundCharacterData FindCharacterDataSet(CharacterDataSet characterDataSet, float tolerance, FoundCharacterData previousCharacterData)
        {

            int startY = Math.Max(0, previousCharacterData.Point.Y + previousCharacterData.CharacterData.HeightFromWritingLine - characterDataSet.MaxHeight - 2);
            int startX = Math.Min(bitmap.Width, previousCharacterData.Point.X + previousCharacterData.CharacterData.Width - 2);
            

            int maxX = startX + characterDataSet.MaxWidth+3;
            int maxY = startY + characterDataSet.MaxHeight * 2;

            for (int y = startY; y < maxY; y++)
            {
                for (int x = startX; x < maxX; x++)
                {
                    CharacterData characterData = CharacterMatchAtPoint(x, y, characterDataSet, tolerance);

                    if (characterData != null)
                    {
                        return new FoundCharacterData
                        {
                            Point = new Point(x, y),
                            CharacterData = characterData
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<FoundTextData> FindCharacterDataSet(CharacterDataSet characterDataSet, float tolerance)
        {
            return InternalFindCharacterDataSet(characterDataSet, tolerance)
                .Select(f =>
                {
                    return new FoundTextData
                    {
                        Bounds = new Rectangle(f.Point, new Size(f.CharacterData.Width, f.CharacterData.Height)),
                        Text = characterDataSet.Letter.ToString()
                    };
                });
        }

        private IEnumerable<FoundCharacterData> InternalFindCharacterDataSet(CharacterDataSet characterDataSet, float tolerance, FoundCharacterData previousCharacterData = null)
        {
            int startX = 0;
            int startY = 0;
            int maxX = neighborDifferences.GetLength(1);
            int maxY = neighborDifferences.GetLength(0);

            if (previousCharacterData != null)
            {
                startY = Math.Max(0, previousCharacterData.Point.Y + previousCharacterData.CharacterData.HeightFromWritingLine - characterDataSet.MaxHeight);
                startX = Math.Min(bitmap.Width, previousCharacterData.Point.X + previousCharacterData.CharacterData.Width - 2);

                maxX = startX + characterDataSet.MaxWidth+3;
                maxY = startY + characterDataSet.MaxHeight * 2;
            }

            List<FoundCharacterData> foundCharacterDatas = new List<FoundCharacterData>();

            for (int y = startY; y < maxY; y++)
            {
                for (int x = startX; x < maxX; x++)
                {
                    CharacterData characterData = CharacterMatchAtPoint(x, y, characterDataSet, tolerance);

                    if (characterData != null)
                    {
                        FoundCharacterData foundCharacterData = new FoundCharacterData
                        {
                            Point = new Point(x, y),
                            CharacterData = characterData
                        };

                        if(!foundCharacterDatas.Any(f => f.Bounds.IntersectsWith(foundCharacterData.Bounds)))
                            yield return foundCharacterData;
                        
                    }
                }
            }
        }



        private CharacterData CharacterMatchAtPoint(int x, int y, CharacterDataSet characterDataSet, float tolerance)
        {
            if (characterDataSet == CharacterDataSet.SpaceCharacterDataSet)
            {
                return CharacterDataSet.SpaceCharacterDataSet.CharacterDatas[0];
            }

            foreach (CharacterData characterData in characterDataSet.CharacterDatas)
            {
                if (IsCharacterMatchAtPoint(x, y, neighborDifferences, characterData, tolerance))
                {
                    return characterData;
                }
            }

            return null;
        }

        public IEnumerable<FoundTextData> FindText(string searchText, OcrData ocrData, float tolerance = 0.05f)
        {
            IEnumerable<FoundCharacterData> findCharacterDataSet = InternalFindCharacterDataSet(ocrData[searchText[0]], tolerance);

            List<FoundTextData> foundText = new List<FoundTextData>();

            foreach (FoundCharacterData foundCharacterData in findCharacterDataSet)
            {
                List<FoundCharacterData> foundCharacterDatas = new List<FoundCharacterData>();
                foundCharacterDatas.Add(foundCharacterData);

                FindText(searchText.Substring(1), ocrData, tolerance, foundCharacterDatas);

                if (foundCharacterDatas.Count < searchText.Length)
                    continue;

                Size wordSize = new Size(foundCharacterDatas.Max(f => f.Bounds.Right)-foundCharacterDatas[0].Bounds.Left,
                    foundCharacterDatas.Max(f => f.CharacterData.Height));

                Point wordStartPoint = new Point(foundCharacterDatas.Min(f => f.Point.X),
                    foundCharacterDatas.Min(f => f.Point.Y));

                Rectangle wordRectangle = new Rectangle(wordStartPoint, wordSize);
                FoundTextData foundTextData = new FoundTextData
                {
                    Bounds = wordRectangle,
                    Text = searchText
                };

                if (foundText.Any(ft => ft.Bounds.IntersectsWith(foundTextData.Bounds)))
                    continue;

                foundText.Add(foundTextData);

                yield return foundTextData;
            }
        }

        private void FindText(string searchText, OcrData ocrData, float tolerance, List<FoundCharacterData> previousCharacterDatas)
        {
            if (string.IsNullOrEmpty(searchText))
                return;

            FoundCharacterData foundCharacterData = FindCharacterDataSet(ocrData[searchText[0]], tolerance, previousCharacterDatas.Last());

            if (foundCharacterData == null)
                return;

            previousCharacterDatas.Add(foundCharacterData);

            FindText(searchText.Substring(1), ocrData, tolerance, previousCharacterDatas);
        }

        public IEnumerable<FoundTextData> FindText(OcrData ocrData, float tolerance = 0.05f)
        {
            List<char> foundChars = new List<char>();

            List<FoundCharacterData> foundDatas = new List<FoundCharacterData>();

            foreach (CharacterDataSet characterDataSet in ocrData.TrainedCharacterDataSets)
            {
                foreach (FoundCharacterData foundCharacterData in InternalFindCharacterDataSet(characterDataSet, tolerance))
                {
                    foundDatas.Add(foundCharacterData);
                    //foundChars.Add(foundCharacterData.CharacterData.Letter);
                    //FindText(ocrData, tolerance, foundCharacterData, foundChars);
                }

            }

            return OrganizeCharacters(foundDatas);
        }

        int bottomTolerance = 2;

        private IEnumerable<FoundTextData> OrganizeCharacters(List<FoundCharacterData> foundCharacterDatas)
        {
            foundCharacterDatas.Sort((f1, f2) =>
            {
                int f1Bottom = f1.Bottom;
                int f2Bottom = f2.Bottom;
                

                if (Math.Abs(f1Bottom - f2Bottom) > bottomTolerance)
                {
                    return f1Bottom.CompareTo(f2Bottom);
                }

                return f1.Point.X.CompareTo(f2.Point.X);
            });

            List<FoundCharacterData> charDatasToRemove = new List<FoundCharacterData>();

            for (int i = 0; i < foundCharacterDatas.Count; i++)
            {
                FoundCharacterData firstCharData = foundCharacterDatas[i];

                if (charDatasToRemove.Contains(firstCharData))
                    continue;

                for (int j = 0; j < foundCharacterDatas.Count; j++)
                {
                    if (i == j)
                        continue;

                    FoundCharacterData secondCharData = foundCharacterDatas[j];

                    if (charDatasToRemove.Contains(secondCharData))
                        continue;

                    if (firstCharData.Bounds.Contains(secondCharData.Bounds))
                    {
                        charDatasToRemove.Add(secondCharData);
                    }
                    else if (firstCharData.Bounds.IntersectsWith(secondCharData.Bounds))
                    {
                        FoundCharacterData neighbor;

                        if (i > 0 && j != i-1)
                            neighbor = foundCharacterDatas[i - 1];
                        else
                        {
                            neighbor = foundCharacterDatas[i + 1];
                        }

                        if (Math.Abs(firstCharData.Bottom - neighbor.Bottom) <
                            Math.Abs(secondCharData.Bottom - neighbor.Bottom))
                        {
                            charDatasToRemove.Add(secondCharData);
                        }
                        else
                        {
                            charDatasToRemove.Add(firstCharData);
                        }
                    }
                }
            }

            foreach (var foundCharacterData in charDatasToRemove)
            {
                foundCharacterDatas.Remove(foundCharacterData);
            }

            FoundTextData foundTextData = null;
            List<FoundCharacterData> prevFoundCharacterDatas = new List<FoundCharacterData>();

            foreach (FoundCharacterData foundCharacterData in foundCharacterDatas)
            {
                if (!prevFoundCharacterDatas.Any())
                {
                    prevFoundCharacterDatas.Add(foundCharacterData);
                    continue;
                }

                FoundCharacterData prevData = prevFoundCharacterDatas.Last();

                if (Math.Abs(foundCharacterData.Bottom - prevData.Bottom) > bottomTolerance
                    || foundCharacterData.Bounds.Left - prevData.Bounds.Right >= 3
                    || foundCharacterData.Bounds.Left < prevData.Bounds.Left)
                {
                    yield return BuildFoundTextData(prevFoundCharacterDatas);
                    prevFoundCharacterDatas = new List<FoundCharacterData>();
                    prevFoundCharacterDatas.Add(foundCharacterData);
                    continue;
                }

                //if (foundCharacterData.Bounds.Left - prevData.Bounds.Right < prevData.Bounds.Width + 1;)
                //{
                //    //TODO HANDLE SPACES HERE
                //    return 
                //}
                prevFoundCharacterDatas.Add(foundCharacterData);
                
            }

            if (prevFoundCharacterDatas.Any())
            {
                yield return BuildFoundTextData(prevFoundCharacterDatas);
            }
        }

        private static FoundTextData BuildFoundTextData(List<FoundCharacterData> prevFoundCharacterDatas)
        {
            if (prevFoundCharacterDatas.Last().Bounds.Right - prevFoundCharacterDatas.First().Bounds.Left < 0)
            {
                
            }
            return new FoundTextData
            {
                Bounds = new Rectangle(prevFoundCharacterDatas.Min(f => f.Point.X),
                    prevFoundCharacterDatas.Min(f => f.Point.Y),
                    prevFoundCharacterDatas.Last().Bounds.Right - prevFoundCharacterDatas.First().Bounds.Left,
                    prevFoundCharacterDatas.Max(f => f.CharacterData.Height)),

               Text = new string(prevFoundCharacterDatas.Select(f => f.Letter).ToArray())
            };
        }

        private void FindText(OcrData ocrData, float tolerance, FoundCharacterData previousCharacterData, List<char> foundChars)
        {
            foreach (CharacterDataSet characterDataSet in ocrData.TrainedCharacterDataSets)
            {
                List<FoundCharacterData> foundCharacterDatas = InternalFindCharacterDataSet(characterDataSet, tolerance, previousCharacterData).ToList();

                if (foundCharacterDatas.Any())
                {
                    foundChars.Add(foundCharacterDatas[0].CharacterData.Letter);
                    FindText(ocrData, tolerance, foundCharacterDatas[0], foundChars);
                }
                    
            }
        }
    }

    public class FoundTextData
    {
        public string Text { get; set; }
        public Rectangle Bounds { get; set; }

        public override string ToString()
        {
            return Text + " [" + Bounds.ToString() + "]";
        }
    }

    public class FoundCharacterData
    {
        public Point Point { get; set; }

        public CharacterData CharacterData { get; set; }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(Point.X, Point.Y, CharacterData.Width, CharacterData.Height);
            }
        }

        public int Bottom
        {
            get
            {
                return Point.Y + CharacterData.WritingLinePosition-1;
            }
        }

        public char Letter
        {
            get
            {
                return CharacterData.Letter;
            }
        }
    }
}
