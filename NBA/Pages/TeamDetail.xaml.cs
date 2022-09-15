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
    /// Логика взаимодействия для TeamDetail.xaml
    /// </summary>
    public partial class TeamDetail : Page
    {
        private Team _currentTeam;
        private static List<AnonimPlayer> _anonimPlayers = new List<AnonimPlayer>();           
        private static List<AnonimMatchup> _anonimMatchups = new List<AnonimMatchup>();
        public TeamDetail(Team team, int index)
        {
            InitializeComponent();
            if(team != null)
                _currentTeam = team;
            
                comboSeason.SelectedIndex = index;


            Name.Content = $"{_currentTeam.TeamName} | {_currentTeam.Division.Name} of {_currentTeam.Division.Conference.Name}";

            comboSeason.ItemsSource = NBAEntities.GetContext().Season.ToList();
            List<PlayerInTeam> RosterContent = NBAEntities.GetContext().PlayerInTeam.Where(t => t.TeamId == _currentTeam.TeamId).ToList();


            for (int i = 0; i < RosterContent.Count; i++)
                _anonimPlayers.Add(new AnonimPlayer(RosterContent[i]));

            List<Matchup> MatchupContent= NBAEntities.GetContext().Matchup.Where(t => t.Team_Home == _currentTeam.TeamId).ToList();


            for (int i = 0; i < MatchupContent.Count; i++)
                _anonimMatchups.Add(new AnonimMatchup(MatchupContent[i]));
            
            DGridRoster.ItemsSource= _anonimPlayers.Where(p => p.Player.Season.Name == comboSeason.Text);
            DGridMatchup.ItemsSource = _anonimMatchups.Where(p => p.Matchup.Season.Name == comboSeason.Text).OrderBy(d=>d.Matchup.Starttime);  
            Logo.Source = new BitmapImage(new Uri($"pack://application:,,,/Images/Logo/{NBAEntities.GetContext().PlayerInTeam.Where(t=>t.TeamId==_currentTeam.TeamId).Select(t=>t.Team.Logo).First()}"));
            PF.ItemsSource = NBAEntities.GetContext().PlayerInTeam.Where(p => p.TeamId == _currentTeam.TeamId && p.Player.Position.Abbr == "PF " && p.Season.Name == comboSeason.Text).OrderBy(p=>p.StarterIndex).Distinct().ToList();
            SG.ItemsSource = NBAEntities.GetContext().PlayerInTeam.Where(p => p.TeamId == _currentTeam.TeamId && p.Player.Position.Abbr == "SG " && p.Season.Name == comboSeason.Text).OrderBy(p => p.StarterIndex).Distinct().ToList();
            C.ItemsSource = NBAEntities.GetContext().PlayerInTeam.Where(p => p.TeamId == _currentTeam.TeamId && p.Player.Position.Abbr == "C  " && p.Season.Name == comboSeason.Text).OrderBy(p => p.StarterIndex).Distinct().ToList();
            PG.ItemsSource = NBAEntities.GetContext().PlayerInTeam.Where(p => p.TeamId == _currentTeam.TeamId && p.Player.Position.Abbr == "PG " && p.Season.Name == comboSeason.Text).OrderBy(p => p.StarterIndex).Distinct().ToList();
            SF.ItemsSource = NBAEntities.GetContext().PlayerInTeam.Where(p => p.TeamId == _currentTeam.TeamId && p.Player.Position.Abbr == "SF " && p.Season.Name == comboSeason.Text).OrderBy(p => p.StarterIndex).Distinct().ToList();

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

            DGridRoster.ItemsSource = _anonimPlayers.Where(p => p.Player.Season.Name == comboSeason.Text);
            DGridMatchup.ItemsSource = _anonimMatchups.Where(p => p.Matchup.Season.Name == comboSeason.Text).OrderBy(d => d.Matchup.Starttime);
            PF.ItemsSource = NBAEntities.GetContext().PlayerInTeam.Where(p => p.TeamId == _currentTeam.TeamId && p.Player.Position.Abbr == "PF " && p.Season.Name == comboSeason.Text).OrderBy(p => p.StarterIndex).Distinct().ToList();
            SG.ItemsSource = NBAEntities.GetContext().PlayerInTeam.Where(p => p.TeamId == _currentTeam.TeamId && p.Player.Position.Abbr == "SG " && p.Season.Name == comboSeason.Text).OrderBy(p => p.StarterIndex).Distinct().ToList();
            C.ItemsSource = NBAEntities.GetContext().PlayerInTeam.Where(p => p.TeamId == _currentTeam.TeamId && p.Player.Position.Abbr == "C  " && p.Season.Name == comboSeason.Text).OrderBy(p => p.StarterIndex).Distinct().ToList();
            PG.ItemsSource = NBAEntities.GetContext().PlayerInTeam.Where(p => p.TeamId == _currentTeam.TeamId && p.Player.Position.Abbr == "PG " && p.Season.Name == comboSeason.Text).OrderBy(p => p.StarterIndex).Distinct().ToList();
            SF.ItemsSource = NBAEntities.GetContext().PlayerInTeam.Where(p => p.TeamId == _currentTeam.TeamId && p.Player.Position.Abbr == "SF " && p.Season.Name == comboSeason.Text).OrderBy(p => p.StarterIndex).Distinct().ToList();

        }
    }
}
