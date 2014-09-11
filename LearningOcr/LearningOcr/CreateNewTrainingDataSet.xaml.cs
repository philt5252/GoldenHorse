using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LearningOcr
{
    /// <summary>
    /// Interaction logic for CreateNewTrainingDataSet.xaml
    /// </summary>
    public partial class CreateNewTrainingDataSet : Window
    {
        public string TrainingDataSetName { get; set; }
        public bool LoadDefaultCharacters { get; set; }

        public CreateNewTrainingDataSet()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            textBox1.Focus();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            TrainingDataSetName = textBox1.Text;
            LoadDefaultCharacters = checkBox1.IsChecked.Value;

            Close();
        }
    }
}
