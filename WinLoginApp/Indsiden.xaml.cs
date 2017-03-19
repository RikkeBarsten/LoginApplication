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

namespace WinLoginApp
{
    /// <summary>
    /// Interaction logic for Indsiden.xaml
    /// </summary>
    public partial class Indsiden : Page
    {


        public Indsiden()
        {
            InitializeComponent();
        }
        
        private void NyKonto_Click(object sender, RoutedEventArgs e)
        {
            OpretBruger opretVindu = new OpretBruger();
            opretVindu.Show();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Uri("Indenfor.xaml", UriKind.RelativeOrAbsolute));

            //Check if userId exists in database, if not, ask user to create account

            //If userId exist, get UserId and password, If password matches, go inside
        }
    }
}
