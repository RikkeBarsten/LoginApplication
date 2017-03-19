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
using System.Windows.Shapes;
using System.Net.Mail;
using System.Data;
using System.Security.Cryptography;

namespace WinLoginApp
{
    /// <summary>
    /// Interaction logic for OpretBruger.xaml
    /// </summary>
    public partial class OpretBruger : Window
    {
        private LoginAppDBDataSet LoginDataSet;
        private LoginAppDBDataSetTableAdapters.LoginInfoTableAdapter LoginTableAdapter;
        

        public OpretBruger()
        {
            InitializeComponent();

            LoginDataSet = new LoginAppDBDataSet();
            LoginTableAdapter = new LoginAppDBDataSetTableAdapters.LoginInfoTableAdapter();
            LoginTableAdapter.Fill(LoginDataSet.LoginInfo);
        }

        


        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            String userId = textBoxId.Text;
            String PW = passwordBox1.Password; 
            String PW2 = passwordBox2.Password;

            if (everythingOK(userId, PW, PW2))
            {

                MessageBox.Show("Everything ok. You are being registered in the system.");
                // Hash password
                var sha1 = new SHA1CryptoServiceProvider();
                var data = Encoding.ASCII.GetBytes(PW);
                var shaPW = sha1.ComputeHash(data);
                // Store ind DB
                LoginAppDBDataSet.LoginInfoRow newRow = LoginDataSet.LoginInfo.NewLoginInfoRow();

                newRow.Id = userId;
                newRow.Kodeord = Convert.ToBase64String(shaPW);
                MessageBox.Show(Convert.ToBase64String(shaPW));

                LoginDataSet.LoginInfo.Rows.Add(newRow);

                LoginTableAdapter.Update(LoginDataSet.LoginInfo);

                // Close newUserWindow
                this.Close();
                
            }
            else
            {

                // Tell user to improve
                // Should give detailed message (is it email, pw or pw2) 
                // So maybe refactor everythingok-method......
                MessageBox.Show("Something went wrong. Please try again.");
            }

        }

        private bool everythingOK(string id, string pw, string pw2)
        {
            // Bools to test if everything is ok before putting user in DB
            bool userIdOK = false;
            bool PWOK = false;
            bool PWmatches = false;

            // Check if user id is email
            if (isValidEmail(id))
            {
                userIdOK = true;
            }

            // Check if PW length and content ok
            if (isValidPW(pw))
            {
                PWOK = true;
            }
            // Check if PWs matches
            if (pw == pw2)
            {
                PWmatches = true;
            }
            // If everything ok return ok
            if (userIdOK == true && PWOK == true && PWmatches == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }






        private bool isValidEmail(string emailAdress)
        {
            try 
            {
                MailAddress m = new MailAddress(emailAdress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool isValidPW(string password)
        {
            bool lengthOK = false;
            bool contentOK = false;

            if (password.Length > 8)
            {
                lengthOK = true;
            }
            if (
                password.Any(char.IsUpper) &&
                password.Any(char.IsNumber)
                )
            {
                contentOK = true;
            }

            if (lengthOK && contentOK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

    }
}
