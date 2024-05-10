using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Логика взаимодействия для bindablePasswordBoxControl.xaml
    /// </summary>
    public partial class bindablePasswordBoxControl : UserControl
    {
        public static readonly DependencyProperty PasswordProperty = 
            DependencyProperty.Register("Password", typeof(SecureString), typeof(bindablePasswordBoxControl));
        public SecureString Password
        {
            get { return (SecureString)GetValue(PasswordProperty);}
            set { SetValue(PasswordProperty, value); }
        }
        public bindablePasswordBoxControl()
        {
            InitializeComponent();
            txtPassword.PasswordChanged += OnPasswordChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = txtPassword.SecurePassword;
        }
    }
}