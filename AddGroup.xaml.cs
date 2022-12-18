using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for AddGroup.xaml
    /// </summary>
    public partial class AddGroup : Window
    {
        int userID;
        string name;
        public AddGroup(string username)
        {
            InitializeComponent();
            name = username;
            using (SqliteConnection connect = new SqliteConnection(@"Data source = DataFile.db"))
            {
                using (SqliteCommand fmd = connect.CreateCommand())
                {
                    connect.Open();
                    fmd.CommandText = @"SELECT Id FROM Users WHERE Name = '" + username + "';";
                    fmd.CommandType = CommandType.Text;
                    SqliteDataReader r = fmd.ExecuteReader();
                    while (r.Read())
                    {
                        userID = r.GetInt32(0);
                    }
                    connect.Close();
                }
            }
        }

        private void SubmitGroup_Click(object sender, RoutedEventArgs e)
        {
            var GroupName = GroupText.Text;
                SqliteConnection connection = new SqliteConnection(@"Data source = DataFile.db");
                connection.Open();
                SqliteCommand command = new SqliteCommand("insert into Groups (GroupName, userID) values ('" + GroupName + "', '" + userID + "');", connection);
                command.ExecuteNonQuery();
                connection.Close();
                MainPage mainpage = new MainPage(name);
                Close();
                mainpage.Show();
        }
    }
}
