using System;
using System.Collections.Generic;
using System.Drawing;
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
using LearningOcr.Core;
using Microsoft.Win32;
using Color = System.Windows.Media.Color;
using Point = System.Drawing.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace LearningOcr
{
    /// <summary>
    /// Interaction logic for OcrWindow.xaml
    /// </summary>
    public partial class OcrWindow : Window
    {
        private string bitmapFile;
        private string sourceBitmapFile;
        Rectangle rect = new Rectangle();
        private Bitmap bitmap;


        public OcrWindow()
        {
            InitializeComponent();
            image1.MouseLeftButtonUp += Image1OnMouseLeftButtonUp;
            rect.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            rect.StrokeThickness = 3;
        }

        private void Image1OnMouseLeftButtonUp(object sender, MouseButtonEventArgs args)
        {
            System.Windows.Point position = args.GetPosition(image1);

            int bucketSize = (int)(image1.ActualHeight / bitmap.Height);

            int bucket = (int)(position.Y/bucketSize);

            rect.Margin = new Thickness(rect.Margin.Left, rect.Margin.Top,
                rect.Margin.Right, ((bitmap.Height-1 - bucket) * bucketSize)+1); 
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Filter = "Bitmap Images |*.bmp";
            bool? showDialog = fileDialog.ShowDialog();

            if (showDialog.HasValue && showDialog.Value)
            {
                bitmapFile = fileDialog.FileName;
                image1.Source = new BitmapImage(new Uri(bitmapFile));
            }
            else
                return;

            this.InvalidateVisual();

            bitmap = new Bitmap(bitmapFile);

            
            rect.HorizontalAlignment = HorizontalAlignment.Left;
            rect.VerticalAlignment = VerticalAlignment.Bottom;
            image1.SizeChanged +=
                (o, args) =>
                {
                    rect.Height = image1.ActualHeight / bitmap.Height;

                    rect.Width = image1.ActualWidth;
                    rect.UpdateLayout();
                };

            
            if(!grid2.Children.Contains(rect))
                grid2.Children.Add(rect);

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Filter = "Bitmap Images |*.bmp";
            bool? showDialog = fileDialog.ShowDialog();

            if (showDialog.HasValue && showDialog.Value)
            {
                sourceBitmapFile = fileDialog.FileName;
                image2.Source = new BitmapImage(new Uri(sourceBitmapFile));
            }
        }

    }
}
