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

namespace NBA.Pages
{
    /// <summary>
    /// Логика взаимодействия для TeamsMain.xaml
    /// </summary>
    public partial class TeamsMain : Page
    {
        public TeamsMain()
        {
            InitializeComponent();
            List<Team> teams = NBAEntities.GetContext().Team.ToList();
           
            for (int i = 0; i < teams.Count(); i++)
                if(!teams[i].Logo.Contains("\\Images\\Logo\\"))
                    teams[i].Logo= teams[i].Logo.Insert(0,"\\Images\\Logo\\");
            
            
            LViewAtlantic.ItemsSource = teams.Where(t => t.Division.Name == "Atlantic").OrderBy(t=>t.TeamName);
            LViewCentral.ItemsSource =teams.Where(t => t.Division.Name == "Centra").OrderBy(t => t.TeamName);
            LViewSoutheast.ItemsSource = teams.Where(t => t.Division.Name == "Southeastern").OrderBy(t => t.TeamName);
            LViewNorthwest.ItemsSource = teams.Where(t => t.Division.Name == "Northwestern").OrderBy(t => t.TeamName);
            LViewPacific.ItemsSource = teams.Where(t => t.Division.Name == "Pacific").OrderBy(t => t.TeamName);
            LViewSouthwest.ItemsSource = teams.Where(t => t.Division.Name == "Southwestern").OrderBy(t => t.TeamName);
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TeamDetail((sender as Button).DataContext as Team, NBAEntities.GetContext().Season.ToList().Max(s => s.SeasonId) - 1));
        }
    }
}
