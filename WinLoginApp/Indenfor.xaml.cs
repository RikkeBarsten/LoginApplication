using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace WinLoginApp
{
    /// <summary>
    /// Interaction logic for Indenfor.xaml
    /// </summary>
    public partial class Indenfor : Page
    {
        public Indenfor()
        {
            InitializeComponent();

            
        }

        private void buttonTestSHA1_Click(object sender, RoutedEventArgs e)
        {
            string pw = textBoxPW.Text;
            // Hash password
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(pw);
            var shaPW = sha1.ComputeHash(data);
            var shaPWString = Convert.ToBase64String(shaPW);

            labelSHA.Content = shaPWString;
            labelSHA.Visibility = Visibility.Visible;
        }

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Indsiden.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
