using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;

namespace LearningOcr.Core.ViewModels
{
    public class OcrTrainingDataViewModel : NotifyPropertyChangedBase
    {
        private string trainingDataName;
        private CharacterDataSet selectedCharacterDataSet;
        private CharacterData selectedCharacterData;
        private ObservableCollection<CharacterData> characterDatas;
        private OcrData ocrData;
        private List<FoundTextData> foundWords;
        private TimeSpan foundTime;
        private string searchText;
        private float tolerance;
        private string foundText;
        private TimeSpan foundAnalyzeTime;
        private List<FoundTextData> foundTextDatas;

        public string TrainingDataName
        {
            get { return ocrData.Name; }
            set
            {
                if (Equals(ocrData.Name, value))
                    return;

                ocrData.Name = value;

                OnPropertyChanged("TrainingDataName");
            }
        }

        public string FoundText
        {
            get { return foundText; }
            set
            {
                foundText = value;
                OnPropertyChanged("FoundText");
            }
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
            }
        }

        public CharacterDataSet SelectedCharacterDataSet
        {
            get { return selectedCharacterDataSet; }
            set
            {
                if (Equals(selectedCharacterDataSet, value))
                    return;

                selectedCharacterDataSet = value;

                OnPropertyChanged("SelectedCharacterDataSet");
                OnPropertyChanged("CharacterDatas");
            }
        }

        public ObservableCollection<CharacterData> CharacterDatas
        {
            get
            {
                if (SelectedCharacterDataSet == null)
                    return null;

                return SelectedCharacterDataSet.CharacterDatas;
            }
        }

        public Bitmap SelectedCharacterDataImage
        {
            get
            {
                if (SelectedCharacterData == null)
                    return null;

                return SelectedCharacterData.Image;
            }
        }

        public float Tolerance
        {
            get { return tolerance; }
            set { tolerance = value; }
        }

        public List<FoundTextData> FoundWords
        {
            get { return foundWords; }
            set
            {
                foundWords = value;
                OnPropertyChanged("FoundWords");
            }
        }

        public TimeSpan FoundTime
        {
            get { return foundTime; }
            set
            {
                foundTime = value;
                OnPropertyChanged("FoundTime");
            }
        }

        public CharacterData SelectedCharacterData
        {
            get { return selectedCharacterData; }
            set
            {
                if (Equals(selectedCharacterData, value))
                    return;

                selectedCharacterData = value;

                OnPropertyChanged("SelectedCharacterData");
                OnPropertyChanged("SelectedCharacterDataImage");
            }
        }

        public ObservableCollection<CharacterDataSet> CharacterDataSets
        {
            get
            {
                return ocrData.CharacterDataSets;
            }
        }

        public OcrTrainingDataViewModel()
        {
            ocrData = new OcrData();
            Tolerance = 0.2f;
            FoundWords = new List<FoundTextData>();
        }

        public void LoadDefaultCharacters()
        {
            for (char ch = 'a'; ch <= 'z'; ch++)
            {
                CharacterDataSets.Add(new CharacterDataSet(ch));
            }

            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                CharacterDataSets.Add(new CharacterDataSet(ch));
            }

            for (char ch = '0'; ch <= '9'; ch++)
            {
                CharacterDataSets.Add(new CharacterDataSet(ch));
            }
        }

        public void AddCharacterImage(Bitmap bitmap)
        {
            CharacterData characterData = new CharacterData(selectedCharacterDataSet.Letter, bitmap);

            SelectedCharacterDataSet.CharacterDatas.Add(characterData);
        }

        public void AddImageFile(string fileName)
        {
            Bitmap bitmap = new Bitmap(fileName);

            AddCharacterImage(bitmap);
        }

        public void Save()
        {
            byte[] serializedData = Serializer.Serialize(ocrData);

            File.WriteAllBytes(ocrData.Name + ".ocr", serializedData);
        }

        public void Load(string fileName)
        {
            OcrData deserializedData = Serializer.DeSerialize(File.ReadAllBytes(fileName)) as OcrData;
            ocrData = deserializedData;

            foreach (string property in this.GetType().GetProperties().Select(p => p.Name))
            {
                OnPropertyChanged(property);
            }
        }

        public void SetPixelCharData(int x, int y, bool isPartOfChar)
        {
            SelectedCharacterData.SetPixelAsCharacterData(x, y, isPartOfChar);
        }

        public bool GetPixelCharData(int x, int y)
        {
            return SelectedCharacterData.IsPixelCharData(x, y);
        }

        public void AnalyzeCharacters()
        {
            ocrData.Analyze();
        }

        public void FindSelectedImage(Bitmap searchBitmap)
        {
            AnalyzeCharacters();
            OcrAnalyzer ocrAnalyzer = new OcrAnalyzer(searchBitmap);
            DateTime dt = DateTime.Now;
            FoundWords = ocrAnalyzer.FindCharacterData(SelectedCharacterData, Tolerance).ToList();
            FoundTime = DateTime.Now - dt;
        }

        public void FindCharacter(Bitmap searchBitmap)
        {
            AnalyzeCharacters();
            OcrAnalyzer ocrAnalyzer = new OcrAnalyzer(searchBitmap);
            DateTime dt = DateTime.Now;
            FoundWords = ocrAnalyzer.FindCharacterDataSet(SelectedCharacterDataSet, Tolerance).ToList();
            FoundTime = DateTime.Now - dt;
        }

        public void FindText(Bitmap searchBitmap)
        {
            AnalyzeCharacters();
            OcrAnalyzer ocrAnalyzer = new OcrAnalyzer(searchBitmap);
            DateTime dt = DateTime.Now;
            FoundWords = ocrAnalyzer.FindText(SearchText, ocrData, Tolerance).ToList();
            FoundTime = DateTime.Now - dt;
        }

        public void AnalyzePicture(Bitmap analyzeBitmap)
        {
            AnalyzeCharacters();
            OcrAnalyzer ocrAnalyzer = new OcrAnalyzer(analyzeBitmap);
            DateTime dt = DateTime.Now;
            FoundWords = ocrAnalyzer.FindText(ocrData, Tolerance).ToList();
            FoundTime = DateTime.Now - dt;
        }
    }
}