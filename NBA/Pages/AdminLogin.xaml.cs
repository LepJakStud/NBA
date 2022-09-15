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
using NBA.Models;
using System.IO;
using Newtonsoft.Json;

namespace NBA.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Page
    {
        public static Admin _currentAdmin;
        public AdminLogin()
        {
            InitializeComponent();
            if (File.Exists("Login.txt"))
            {
                Remember.IsChecked = true;
                using(StreamReader sr = new StreamReader("Login.txt"))
                {
                    Jobnumber.Text = sr.ReadLine();
                    Password.Password = sr.ReadLine();
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            _currentAdmin = NBAEntities.GetContext().Admin.Where(a => a.Jobnumber == Jobnumber.Text && a.Passwords == Password.Password).FirstOrDefault();
            if (_currentAdmin == null)
            {
                MessageBox.Show("Неверно введен логин или пароль");
                return;
            }
            if (Remember.IsChecked == true)
                Remember_Checked();
            else
                File.Delete("Login.txt");
            if (_currentAdmin.RoleId == "1")
                Manager.MainFrame.Navigate(new EventAdministratorMenu());
            else
                Manager.MainFrame.Navigate(new TechnicalAdministratorMenu());
        }

        private void Remember_Checked()
        {
            using(StreamWriter sw = new StreamWriter("Login.txt"))
            {
                sw.WriteLine($"{Jobnumber.Text}");
                sw.WriteLine($"{Password.Password}");
            }
        }
    }
}