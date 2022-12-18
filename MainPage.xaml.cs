using Microsoft.Data.Sqlite;
using PasswordManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        int groupID;
        string user;
        int userID;
        public MainPage(string username)
        {
            InitializeComponent();
            user = username;
            GetUserID();
            List<string> groups = getGroups(username);
            foreach (string group in groups)
            {
                Button newBtn = new Button();

                newBtn.Content = group;
                newBtn.Name = "Button" + group;
                newBtn.Width = 100;
                newBtn.Height = 50;
                newBtn.Click += new RoutedEventHandler(groupButton_Click);
                groupBtns.Children.Add(newBtn);

                void groupButton_Click(object sender, EventArgs e)
                {
                    passwordBtns.Children.Clear();
                    passwords.Children.Clear();
                    Button button = sender as Button;
                    using (SqliteConnection connect = new SqliteConnection(@"Data source = DataFile.db"))
                    {
                        using (SqliteCommand fmd = connect.CreateCommand())
                        {
                            connect.Open();
                            fmd.CommandText = @"SELECT Id FROM Groups WHERE GroupName = '" + button.Content + "';";
                            fmd.CommandType = CommandType.Text;
                            SqliteDataReader r = fmd.ExecuteReader();
                            while (r.Read())
                            {
                                groupID = r.GetInt32(0);
                                List<string> accounts = getUsername(userID, groupID);
                                foreach (string account in accounts)
                                {
                                    Button newBtn = new Button();

                                    newBtn.Content = account;
                                    newBtn.Click += new RoutedEventHandler(passwordButton_Click);
                                    passwordBtns.Children.Add(newBtn);

                                    void passwordButton_Click(object sender, EventArgs e)
                                    {
                                        passwords.Children.Clear();
                                        Button button = sender as Button;
                                        using (SqliteConnection connect = new SqliteConnection(@"Data source = DataFile.db"))
                                        {
                                            using (SqliteCommand fmd = connect.CreateCommand())
                                            {
                                                connect.Open();
                                                fmd.CommandText = @"SELECT password FROM passwords WHERE username = '" + button.Content + "';";
                                                fmd.CommandType = CommandType.Text;
                                                SqliteDataReader r = fmd.ExecuteReader();
                                                while (r.Read())
                                                {
                                                    string password = r.GetString(0);
                                                    Label label = new Label();
                                                    label.Content = password;
                                                    passwords.Children.Add(label);
                                                }
                                            }
                                        }
                                    }
                                }                                 
                            }
                            connect.Close();
                            lbPasswords.Visibility = Visibility.Visible;
                            addNewUsername.Visibility = Visibility.Visible;
                        }
                    }
                }
                
            }
        }
        public static List<string> getGroups(string username)
        {
            List<string> items = new List<string>();
            using (SqliteConnection connect = new SqliteConnection(@"Data source = DataFile.db"))
            {
                using (SqliteCommand fmd = connect.CreateCommand())
                {
                    connect.Open();
                    fmd.CommandText = @"SELECT Groups.GroupName FROM Groups JOIN Users ON Groups.userID = Users.Id WHERE Users.Name = '" + username + "'";
                    fmd.CommandType = CommandType.Text;
                    SqliteDataReader r = fmd.ExecuteReader();
                    while (r.Read())
                    {
                        items.Add(r.GetString(0));
                    }
                    connect.Close();
                }
            }
            return items;

        }

        public void GetUserID()
        {
            using (SqliteConnection connect = new SqliteConnection(@"Data source = DataFile.db"))
            {
                using (SqliteCommand fmd = connect.CreateCommand())
                {
                    connect.Open();
                    fmd.CommandText = @"SELECT Id FROM Users WHERE Name = '" + user + "';";
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
   

        public static List<string> getUsername(int userID, int groupID)
        {
            List<string> usernames = new List<string>();

            using (SqliteConnection connect = new SqliteConnection(@"Data source = DataFile.db"))
            {
                using (SqliteCommand fmd = connect.CreateCommand())
                {
                    connect.Open();
                    fmd.CommandText = @"SELECT passwords.username FROM passwords JOIN Groups ON passwords.groupID = Groups.Id WHERE Groups.userID = " + userID + " AND Groups.Id = " + groupID;
                    fmd.CommandType = CommandType.Text;
                    SqliteDataReader r = fmd.ExecuteReader();
                    while (r.Read())
                    {
                        usernames.Add(r.GetString(0));
                    }
                    connect.Close();
                }
            }
            return usernames;
        }

        public void addNewUsername_Click(object sender, RoutedEventArgs e)
        {
            AddAccounts addAcc = new AddAccounts(groupID);
            addAcc.Show();
        }

        private void addNewGroup_Click(object sender, RoutedEventArgs e)
        {
            AddGroup addGroup = new AddGroup(user);
            Close();
            addGroup.Show();
        }
    }
}
