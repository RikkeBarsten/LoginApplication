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
    /// Interaction logic for Indsiden.xaml
    /// </summary>
    public partial class Indsiden : Page
    {
        private LoginAppDBDataSet LoginDataSet;
        private LoginAppDBDataSetTableAdapters.LoginInfoTableAdapter LoginTableAdapter;

        public Indsiden()
        {
            InitializeComponent();

            LoginDataSet = new LoginAppDBDataSet();
            LoginTableAdapter = new LoginAppDBDataSetTableAdapters.LoginInfoTableAdapter();
            LoginTableAdapter.Fill(LoginDataSet.LoginInfo);

            this.ShowsNavigationUI = false;
            
        }
        
        private void NyKonto_Click(object sender, RoutedEventArgs e)
        {
            OpretBruger opretVindu = new OpretBruger();
            opretVindu.Closed += (s, eventargs) =>
            {
                LoginDataSet = new LoginAppDBDataSet();
                LoginTableAdapter = new LoginAppDBDataSetTableAdapters.LoginInfoTableAdapter();
                LoginTableAdapter.Fill(LoginDataSet.LoginInfo);
                
            };
            opretVindu.Show();
            
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            

            //Check if userId exists in database, if not, ask user to create account
            String userId = textBoxUsername.Text;
            
            String PW = passwordBox.Password;

            // Hash password
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(PW);
            var shaPW = sha1.ComputeHash(data);
            var shaPWString = Convert.ToBase64String(shaPW);
            



            bool IdExists = LoginDataSet.LoginInfo.Rows.Contains(userId);
            

            LoginAppDBDataSet.LoginInfoRow DBID_row = (LoginAppDBDataSet.LoginInfoRow)LoginDataSet.LoginInfo.Rows.Find(userId);


            if (DBID_row != null)
            {
                var DBID = DBID_row.Id;
            }
            
           
           


            if (!IdExists)
            {
                MessageBox.Show("Dit brugernavn er ukendt. Hvis du ikke har brugt app'en før, venligst opret bruger.");
            }

            //If userId exist, get UserId and password, If password matches, go inside
            else
            {
                //Get row
                LoginAppDBDataSet.LoginInfoRow userRow = (LoginAppDBDataSet.LoginInfoRow)LoginDataSet.LoginInfo.Rows.Find(userId);
                

                //Get PW
                String DBPW = userRow.Kodeord;

                if (DBPW == shaPWString)
                {
                    this.NavigationService.Navigate(new Uri("Indenfor.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MessageBox.Show("Har du glemt dit kodeord? Så kan du ikke logge ind.");
                }
            }
        }
    }
}
