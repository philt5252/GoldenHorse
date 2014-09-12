using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LearningOcr.Core;
using LearningOcr.Core.ViewModels;
using Microsoft.Win32;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace LearningOcr
{
    /// <summary>
    /// Interaction logic for OcrTrainingDataWindow.xaml
    /// </summary>
    public partial class OcrTrainingDataWindow : Window
    {
        private OcrTrainingDataViewModel viewModel;
        private Point topLeft = new Point();
        private Point bottomRight = new Point();
        private bool firstPointSet = false;

        public OcrTrainingDataWindow()
        {
            InitializeComponent();
            viewModel = new OcrTrainingDataViewModel();
            DataContext = viewModel;

            Loaded += OnLoaded;

            listBox2.SelectionChanged += ListBox2OnSelectionChanged;

            image2.MouseRightButtonUp += Image2OnMouseRightButtonUp;
        }

        private void Image2OnMouseRightButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            Point position = mouseButtonEventArgs.GetPosition(image2);

            if (!firstPointSet)
            {
                topLeft = position;
                firstPointSet = true;
            }
            else
            {
                bottomRight = position;
                firstPointSet = false;

                Bitmap bitmap = BitmapSourceToBitmap2(image2.Source as BitmapSource);
                Bitmap letterBitmap =
                    bitmap.Clone(
                        new System.Drawing.Rectangle((int)topLeft.X, (int)topLeft.Y,(int) bottomRight.X - (int) topLeft.X+1,
                            (int) bottomRight.Y - (int) topLeft.Y+1), bitmap.PixelFormat);


                viewModel.AddCharacterImage(letterBitmap);
            }
                
        }

        private void ListBox2OnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (listBox2.SelectedItem == null)
                return;
            //System.Drawing.Rectangle selectedRect = (System.Drawing.Rectangle)listBox2.SelectedItem;

            //Rectangle rectangle = rectDict[selectedRect];
            //scrollViewer1.BringIntoView(new Rect(Canvas.GetLeft(rectangle), Canvas.GetTop(rectangle), rectangle.Width, rectangle.Height));

            foreach (Rectangle rectangle in rectDict.Values)
            {
                rectangle.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }

            rectDict[((FoundTextData) listBox2.SelectedItem).Bounds].Stroke =
                new SolidColorBrush(Color.FromRgb(255, 255, 0));


        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            viewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "SelectedCharacterDataImage")
            {
                UpdateRects();
                UpdateWritineLineIndicator();
            }

            if (args.PropertyName == "FoundWords")
            {
                UpdateWordRects();
            }
        }

        private Dictionary<System.Drawing.Rectangle, Rectangle> rectDict =
            new Dictionary<System.Drawing.Rectangle, Rectangle>(); 
        private void UpdateWordRects()
        {
            if (image2.Source == null)
                return;

            double width = image2.Source.Width;
            double height = image2.Source.Height;

            double widthPixelSize = image2.ActualWidth / width;
            double heightPixelSize = image2.ActualHeight / height;

            rectDict = new Dictionary<System.Drawing.Rectangle, Rectangle>(); 

            foreach (Rectangle rectangle in testImageCnv.Children.OfType<Rectangle>().ToList())
            {
                testImageCnv.Children.Remove(rectangle);
            }

            foreach (FoundTextData foundWord in viewModel.FoundWords)
            {
                Rectangle showRect = new Rectangle();
                showRect.HorizontalAlignment = HorizontalAlignment.Left;
                showRect.VerticalAlignment = VerticalAlignment.Top;
                showRect.Width = foundWord.Bounds.Width * widthPixelSize;
                showRect.Height = foundWord.Bounds.Height* heightPixelSize;

                showRect.Stroke = new SolidColorBrush(Color.FromRgb(255,100,100));
                showRect.StrokeThickness = 1;

                testImageCnv.Children.Add(showRect);

                Canvas.SetLeft(showRect, foundWord.Bounds.X*widthPixelSize);
                Canvas.SetTop(showRect, foundWord.Bounds.Y*heightPixelSize);
                rectDict[foundWord.Bounds] = showRect;
            }
        }

        private Rectangle writingLineRectangle;
        private void UpdateWritineLineIndicator()
        {
            if (writingLineRectangle != null)
                imageGrid.Children.Remove(writingLineRectangle);

            writingLineRectangle = new Rectangle();
            writingLineRectangle.HorizontalAlignment = HorizontalAlignment.Left;
            writingLineRectangle.VerticalAlignment = VerticalAlignment.Top;
            writingLineRectangle.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            writingLineRectangle.StrokeThickness = 2;

            imageGrid.Children.Add(writingLineRectangle);

            image1.UpdateLayout();

            if (image1.Source == null)
            {
                writingLineRectangle.Visibility = Visibility.Hidden;
                return;
            }

            writingLineRectangle.Visibility = Visibility.Visible;

            writingLineRectangle.Width = image1.ActualWidth + 50;
            double heightPixelSize = image1.ActualHeight/image1.Source.Height;

            int writingLinePosition = viewModel.SelectedCharacterData.WritingLinePosition;

            double pos = (writingLinePosition+1)*heightPixelSize-2;

            writingLineRectangle.Margin = new Thickness(-25, pos, 0, 0);
            writingLineRectangle.UpdateLayout();
        }

        private void NewTrainingDataOnLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CreateNewTrainingDataSet createWindow = new CreateNewTrainingDataSet();
            createWindow.ShowDialog();

            viewModel.TrainingDataName = createWindow.TrainingDataSetName;

            if (createWindow.LoadDefaultCharacters)
                viewModel.LoadDefaultCharacters();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();

            bool? showDialog = fileDialog.ShowDialog();

            if (!showDialog.HasValue
                || !showDialog.Value)
                return;

            viewModel.AddImageFile(fileDialog.FileName);
        }

        private void SaveMenuItemOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            viewModel.Save();
        }

        private void OpenMenuItemOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            bool? showDialog = openFileDialog.ShowDialog();

            if (!showDialog.HasValue
                || !showDialog.Value)
            {
                return;
            }

            viewModel.Load(openFileDialog.FileName);
        }

        private void Image1_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double width = image1.Source.Width;
            double height = image1.Source.Height;

            Point position = e.GetPosition(image1);

            double widthPixelSize = image1.ActualWidth / width;
            double heightPixelSize = image1.ActualHeight / height;

            int posXPixel = (int)(position.X / widthPixelSize);
            int posYPixel = (int)(position.Y / heightPixelSize);

            bool pixelCharData = viewModel.GetPixelCharData(posXPixel, posYPixel);

            viewModel.SetPixelCharData(posXPixel, posYPixel, !pixelCharData);

            xRects[posYPixel, posXPixel].Visibility = !pixelCharData ? Visibility.Hidden : Visibility.Visible;
        }

        private void UpdateRects()
        {
            image1.UpdateLayout();
            
            foreach (Rectangle rectangle in imageGrid.Children.OfType<Rectangle>().ToList())
            {
                imageGrid.Children.Remove(rectangle);
            }

            if (image1.Source == null)
                return;

            double width = image1.Source.Width;
            double height = image1.Source.Height;

            int widthInt = (int) Math.Round(height);
            int heightInt = (int) Math.Round(width);

            double widthPixelSize = image1.ActualWidth / width;
            double heightPixelSize = image1.ActualHeight / height;

            xRects = new Rectangle[widthInt, heightInt];

            for (int y = 0; y < (int)widthInt; y++)
            {
                for (int x = 0; x < (int)heightInt; x++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Height = heightPixelSize;
                    rectangle.Width = widthPixelSize;
                    rectangle.Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    rectangle.StrokeThickness = 3;
                    rectangle.Margin = new Thickness(x * widthPixelSize, y * heightPixelSize, 0, 0);
                    rectangle.HorizontalAlignment = HorizontalAlignment.Left;
                    rectangle.VerticalAlignment = VerticalAlignment.Top;
                    rectangle.Fill = new SolidColorBrush(Color.FromArgb(150, 255, 0, 0));
                    xRects[y, x] = rectangle;
                    imageGrid.Children.Add(rectangle);

                    bool pixelCharData = viewModel.GetPixelCharData(x, y);

                    rectangle.Visibility = pixelCharData ? Visibility.Hidden : Visibility.Visible;

                    rectangle.PreviewMouseLeftButtonUp += RectangleOnPreviewMouseLeftButtonUp;
                }
            }
        }

        private void RectangleOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            mouseButtonEventArgs.Handled = false;
            Image1_OnPreviewMouseLeftButtonUp(sender, mouseButtonEventArgs);
            return;
        }

        private Rectangle[,] xRects;
        private void Image1_OnSourceUpdated(object sender, DataTransferEventArgs e)
        {
            
        }

        private void Image1_OnPreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            double height = image1.Source.Height;

            Point position = e.GetPosition(image1);

            double heightPixelSize = image1.ActualHeight / height;
            int posYPixel = (int)(position.Y / heightPixelSize);

            writingLineRectangle.Margin = new Thickness(writingLineRectangle.Margin.Left, (posYPixel+1)*heightPixelSize-2, 0, 0);
            viewModel.SelectedCharacterData.WritingLinePosition = posYPixel;
        }

        private void AnalyzeCharactersOnClick(object sender, RoutedEventArgs e)
        {
            viewModel.AnalyzeCharacters();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files |*.bmp;*.jpg;*.png";

            bool? showDialog = openFileDialog.ShowDialog();

            if (!showDialog.HasValue || !showDialog.Value)
                return;

            image2.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            searchBitmap = new Bitmap(openFileDialog.FileName);
        }

        private Bitmap searchBitmap;
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FindSelectedImage(searchBitmap);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FindCharacter(searchBitmap);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FindText(searchBitmap);
        }

        private double currentZoom1 = 1;

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double zoom = (e.NewValue - e.OldValue) * 0.33;

            if (double.IsNaN(viewbox1.Height))
                return;


            viewbox1.Height *= (1 + zoom);
            viewbox1.Width *= (1 + zoom);
        }

        private void button7_Click_1(object sender, RoutedEventArgs e)
        {
            viewModel.AnalyzePicture(searchBitmap);
        }

        public static System.Drawing.Bitmap BitmapSourceToBitmap2(BitmapSource srs)
        {
            int width = srs.PixelWidth;
            int height = srs.PixelHeight;
            int stride = width * ((srs.Format.BitsPerPixel + 7) / 8);
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(height * stride);
                srs.CopyPixels(new Int32Rect(0, 0, width, height), ptr, height * stride, stride);
                using (var btm = new System.Drawing.Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format32bppRgb, ptr))
                {
                    // Clone the bitmap so that we can dispose it and
                    // release the unmanaged memory at ptr
                    return new System.Drawing.Bitmap(btm);
                }
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
