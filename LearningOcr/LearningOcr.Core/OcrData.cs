using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using LearningOcr.Core.ViewModels;

namespace LearningOcr.Core
{
    [Serializable]
    public class OcrData : NotifyPropertyChangedBase
    {
        private Dictionary<char, CharacterDataSet> characterDataDict = new Dictionary<char, CharacterDataSet>(); 
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (Equals(name, value))
                    return;

                name = value;

                OnPropertyChanged("Name");
            }
        }

        public ObservableCollection<CharacterDataSet> CharacterDataSets { get; protected set; }

        public CharacterDataSet[] TrainedCharacterDataSets
        {
            get
            {
                return CharacterDataSets.Where(c => c.CharacterDatas.Any()).ToArray();
            }
        }

        public OcrData()
        {
            CharacterDataSets = new ObservableCollection<CharacterDataSet>();

            CharacterDataSets.CollectionChanged += CharacterDataSetsOnCollectionChanged;
        }

        private void CharacterDataSetsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (CharacterDataSet characterDataSet in args.NewItems)
                {
                    characterDataDict[characterDataSet.Letter] = characterDataSet;
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (CharacterDataSet characterDataSet in args.OldItems)
                {
                    characterDataDict.Remove(characterDataSet.Letter);
                }
            }
        }

        public CharacterDataSet this[char ch]
        {
            get
            {
                return characterDataDict[ch];
            }
        }

        public void Analyze()
        {
            foreach (CharacterDataSet characterDataSet in CharacterDataSets)
            {
                foreach (CharacterData characterData in characterDataSet.CharacterDatas)
                {
                    characterData.Analyze();
                }
            }
        }
    }
}