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
    /// Логика взаимодействия для TeamReport.xaml
    /// </summary>
    public partial class TeamReport : Page
    {
        public List<Team> _allTeams = NBAEntities.GetContext().Team.ToList();
        public TeamReport()
        {
            InitializeComponent();
            DGridAvgTeams.ItemsSource = _allTeams;
            DGridTotalTeams.ItemsSource = _allTeams;
            comboView.ItemsSource=new List<string>() { "Average","Total" };
            comboView.SelectedIndex=0;
            comboType.ItemsSource=new List<string>() { "Preseason", "Regular Season", "Post Season " };
            comboType.SelectedIndex=0;
            comboRank.ItemsSource = new List<string>() { "Points", "Rebounds", "Assists", "Steals", "Blocks", "Turnovers", "Fouls" };
            comboRank.SelectedIndex=0;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            //var currentSort = _allTeams;
            //switch (comboType.Text)
            //{
            //    case "Preseason":
            //        currentSort = currentSort.Where(t => t.Matchup.Where(m => m.MatchupType.Name == "Preseason").Select(m => m.MatchupType.Name).FirstOrDefault() == "Preseason").ToList();
            //        break;
            //    case "Regular Season":
            //        currentSort = currentSort.Where(t => t.Matchup.Where(m => m.MatchupType.Name == "Regular Season").Select(m => m.MatchupType.Name).FirstOrDefault() == "Regular Season").ToList();
            //        break;
            //        default:
            //        currentSort = currentSort.Where(t => t.Matchup.Where(m => m.MatchupType.Name != "Regular Season").Select(m => m.MatchupType.Name).FirstOrDefault() != "Regular Season" &&
            //        t.Matchup.Where(m => m.MatchupType.Name != "Preseason").Select(m => m.MatchupType.Name).FirstOrDefault() != "Preseason").ToList();
            //        break;
            //}
            //if(comboView.Text== "Average")
            //{
            //    DGridAvgTeams.Visibility = Visibility.Visible;
            //    DGridTotalTeams.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    DGridTotalTeams.Visibility = Visibility.Visible;
            //    DGridAvgTeams.Visibility = Visibility.Hidden;
            //}
        }
    }
}
