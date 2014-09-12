using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace LearningOcr.Core
{
    [Serializable]
    public class CharacterDataSet
    {
        private char letter;

        public char Letter
        {
            get { return letter; }
            protected set { letter = value; }
        }

        public static CharacterDataSet SpaceCharacterDataSet = new CharacterDataSet(' ');

        public ObservableCollection<CharacterData> CharacterDatas { get; protected set; }

        public int MaxHeight
        {
            get
            {
                if (!CharacterDatas.Any())
                    return 0;

                return CharacterDatas.Max(c => c.Image.Height-(c.Image.Height-c.WritingLinePosition+1));
            }
        }

        public int MaxWidth
        {
            get
            {
                if (!CharacterDatas.Any())
                    return 0;

                return CharacterDatas.Max(c => c.Image.Width);
            }
        }

        public CharacterDataSet(char letter)
        {
            Letter = letter;
            CharacterDatas = new ObservableCollection<CharacterData>();
        }
    }
}