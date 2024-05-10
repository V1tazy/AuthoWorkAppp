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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AuthoWorkAppp.Assets.customElement
{
    /// <summary>
    /// Логика взаимодействия для XromWindowControl.xaml
    /// </summary>
    public partial class XromWindowControl : UserControl
    {
        public string Header { get; set; }

        public ImageSource imageSource { get; set; }
        public XromWindowControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AdjustWindowSize()
        {
            if (Window.GetWindow(this).WindowState == WindowState.Maximized)
            {
                Window.GetWindow(this).WindowState = WindowState.Normal;
            }
            else
            {
                Window.GetWindow(this).WindowState = WindowState.Maximized;
            }

        }
    }
}
