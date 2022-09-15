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
    /// Логика взаимодействия для VisitorMenu.xaml
    /// </summary>
    public partial class VisitorMenu : Page
    {
        public VisitorMenu()
        {
            InitializeComponent();
            btnTeams.Background = Main.color;
            btnPlayers.Background = Main.color;
            btnMatchups.Background = Main.color;
            btnPhotos.Background = Main.color;
        }

        private void btnTeams_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri(@"\Pages\TeamsMain.xaml", UriKind.Relative));
            
        }
        private void btnPlayers_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PlayersMain());
        }
        private void btnMatchups_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new MatchupMain());
        }

        private void btnPhotos_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Photos());
        }

    }
}
