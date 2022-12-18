using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddAccounts.xaml
    /// </summary>
    public partial class AddAccounts : Window
    {
        int group;
        public AddAccounts(int groupID)
        {
            InitializeComponent();
            group = groupID;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var Username = AccountText.Text;
            var Password = passwordText.Password;
            var PasswordCopy = passwordText_Copy.Password;
            if (Password == PasswordCopy)
            {
                SqliteConnection connection = new SqliteConnection(@"Data source = DataFile.db");
                connection.Open();
                SqliteCommand command = new SqliteCommand("insert into passwords (username, password, groupID) values ('" + Username + "', '" + Password + "', '" + group + "');", connection);
                command.ExecuteNonQuery();
                connection.Close();
                Close();
            }
            else
            {
                MessageBox.Show("Passwords don't match");
            }
        }
    }
}
