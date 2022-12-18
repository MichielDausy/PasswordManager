using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.Xml.Linq;

namespace PasswordManager
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

        public void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var Username = UsernameText.Text;
            var Password = passwordText.Password;

            using (UserDataContext context = new UserDataContext()) 
            {
                bool userFound = context.Users.Any(user => user.Name == Username && user.Password == Password);

                if (userFound)
                {
                    GrantAccess(Username);
                    Close();
                }
                else
                {
                    MessageBox.Show("User Not Found");
                }
            }
        }

        public void GrantAccess(string username)
        {
            MainPage main = new MainPage(username);
            main.Show();
        }
        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            Close();
            register.Show();
        }
    }
}
