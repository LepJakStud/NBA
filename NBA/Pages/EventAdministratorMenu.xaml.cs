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

namespace NBA.Pages
{
    /// <summary>
    /// Логика взаимодействия для EventAdministratorMenu.xaml
    /// </summary>
    public partial class EventAdministratorMenu : Page
    {
        public EventAdministratorMenu()
        {
            InitializeComponent();
        }

        private void btnSeasons_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMatchups_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ManageMatchups(2,DateTime.Now,false));
        }

        private void btnTeams_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ManageTeams());
        }

        private void btnPlayers_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ManagePlayers());
        }
    }
}
