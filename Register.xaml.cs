using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var Username = UsernameText.Text;
            var Password = passwordText.Password;
            var PasswordCopy = passwordText_Copy.Password;
            if (Password == PasswordCopy)
            {
                SqliteConnection connection = new SqliteConnection(@"Data source = DataFile.db");
                connection.Open();
                SqliteCommand command = new SqliteCommand("insert into Users (Name, Password) values ('" + Username + "', '" + Password + "');", connection);
                command.ExecuteNonQuery();
                connection.Close();
                MainWindow mainWindow = new MainWindow();
                Close();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Passwords don't match");
            }
        }
    }
}
