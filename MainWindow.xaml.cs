using System;
using System.Configuration;
using System.Windows;
using System.Data.SQLite;


namespace ProfileApp
{
    public partial class MainWindow : Window
    {
        //string conn = ConfigurationManager.AppSettings["con"]; //берем запрос из конфига App.config DB

        private string connectionString = "Data Source=C:\\base\\data\\db.db;Version=3;";
        //private string connectionString = "conn";

        public class Profile
        {
            public string Name { get; set; }
            public List<Contact> Contacts { get; set; }
        }

        public class Contact
        {
            public string Address { get; set; }
            public string Phone { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadProfilesFromDatabase();
        }

        private void LoadProfilesFromDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT * FROM Profiles", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["Name"].ToString();
                    string address = reader["Address"].ToString();
                    string phone = reader["Phone"].ToString();

                    var profile = new Profile { Name = name, Contacts = new List<Contact>() };
                    profile.Contacts.Add(new Contact { Address = address, Phone = phone });
                    profileTreeView.Items.Add(profile);
                }
            }
        }

        private void AddProfile_Click(object sender, RoutedEventArgs e)
        {
            // Здесь можно добавить логику для добавления нового профиля в базу данных и в TreeView
        }

        private void DeleteProfile_Click(object sender, RoutedEventArgs e)
        {
            if (profileTreeView.SelectedItem != null)
            {
                // Здесь можно добавить логику для удаления профиля из базы данных и из TreeView
                profileTreeView.Items.Remove(profileTreeView.SelectedItem);
            }
        }
    }
}