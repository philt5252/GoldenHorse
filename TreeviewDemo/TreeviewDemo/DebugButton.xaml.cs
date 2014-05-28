using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreeviewDemo
{
    /// <summary>
    /// Interaction logic for DebugButton.xaml
    /// </summary>
    public partial class DebugButton : UserControl
    {
        public static readonly DependencyProperty IsDebuggingProperty = DependencyProperty.Register(
            "IsDebugging", typeof (bool), typeof (DebugButton), new PropertyMetadata(default(bool)));

        public bool IsDebugging
        {
            get { return (bool) GetValue(IsDebuggingProperty); }
            set { SetValue(IsDebuggingProperty, value); }
        }

        public DebugButton()
        {
            InitializeComponent();
        }

        private void ChangeOpacity(object sender, RoutedEventArgs e)
        {
            int opacity;
            if (IsDebugging)
            {
                IsDebugging = false;
                opacity = 0;
            }
            else
            {
                IsDebugging = true;
                opacity = 1;
            }

            DoubleAnimation da = new DoubleAnimation(opacity, new Duration(new TimeSpan(0,0,0,0,1)));
            debugBtn.BeginAnimation(OpacityProperty, da);
        }
    }
}
