using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LearningOcr.Core;
using Point = System.Drawing.Point;

namespace LearningOcr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Bitmap findBitmap = new Bitmap("Images\\RunTask.png");
            Bitmap sourceBitmap = new Bitmap("Images\\TradingManagerScn.png");

            DateTime dateTime = DateTime.Now;
            //BitmapFinder bitmapFinder = new BitmapFinder(BitmapSourceToBitmap2(imageSource), BitmapSourceToBitmap2(image2Source));
            BitmapFinder bitmapFinder = new BitmapFinder(sourceBitmap, findBitmap);
            
            Point point = bitmapFinder.Find();

            TimeSpan timeSpan = DateTime.Now - dateTime;

            pointTbx.Text = point.ToString();
            timeTbx.Text = timeSpan.ToString();
        }

        public static System.Drawing.Bitmap BitmapSourceToBitmap2(BitmapSource srs)
        {
            System.Drawing.Bitmap btm = null;
            int width = srs.PixelWidth;
            int height = srs.PixelHeight;
            int stride = width * ((srs.Format.BitsPerPixel + 7) / 8);
            IntPtr ptr = Marshal.AllocHGlobal(height * stride);
            srs.CopyPixels(new Int32Rect(0, 0, width, height), ptr, height * stride, stride);
            btm = new System.Drawing.Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format8bppIndexed, ptr);
            return btm;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            OcrWindow ocrWindow = new OcrWindow();
            ocrWindow.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            OcrTrainingDataWindow ocrTrainingDataWindow = new OcrTrainingDataWindow();
            ocrTrainingDataWindow.Show();
        }
    }
}
