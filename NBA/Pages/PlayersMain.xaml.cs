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
    /// Логика взаимодействия для PlayersMain.xaml
    /// </summary>
    public partial class PlayersMain : Page
    {

        private static int _indexInitial = 0;
        private static string[] _initial = new string[3];
        public int _top10 = 10;
        private static List<AnonimPlayer> _allPlayers = new List<AnonimPlayer>();
        public PlayersMain()
        {
            InitializeComponent();

            comboSeason.ItemsSource = NBAEntities.GetContext().Season.ToList();
            comboSeason.SelectedIndex = NBAEntities.GetContext().Season.ToList().Max(s => s.SeasonId) - 1;

            comboTeams.ItemsSource = NBAEntities.GetContext().Team.ToList().OrderBy(t => t.TeamName);

            List<PlayerInTeam> allInlTeams = NBAEntities.GetContext().PlayerInTeam.ToList().Where(p => p.Season.Name == comboSeason.Text).ToList();
            if(_allPlayers.Count==0)
            for (int i = 0; i < allInlTeams.Count; i++)
            {
                _allPlayers.Add(new AnonimPlayer(allInlTeams[i]));
                if (_allPlayers[i].Player.Player.Img.Contains("jpg") && !_allPlayers[i].Player.Player.Img.Contains("\\Images\\Players\\"))
                    _allPlayers[i].Player.Player.Img = _allPlayers[i].Player.Player.Img.Insert(0, "\\Images\\Players\\");
            }
            _allPlayers = _allPlayers.OrderBy(p => Convert.ToInt32(p.Player.ShirtNumber)).ToList();
            DGridPlayers.ItemsSource = _allPlayers.Take(10);

            ofPosition.Content = $" of {_allPlayers.Count / 10}";
            if (_allPlayers.Count % 10 != 0)
                ofPosition.Content = $" of {_allPlayers.Count / 10 + 1}";
            Position.Text = "1";
            Total.Content = Total.Content.ToString().Replace("XX", $"{_allPlayers.Count}");
            Total.Content = Total.Content.ToString().Replace("YY", $"10");
        }
        private void Letter_Click(object sender, RoutedEventArgs e)
        {
            string source = e.Source.ToString().Replace("System.Windows.Controls.Button: ", null);
            if (source == "All" || _indexInitial == 2)
            {
                DGridPlayers.ItemsSource = _allPlayers.OrderBy(p => p.Player.ShirtNumber);
                _indexInitial = 0;
                return;
            }
            if (_indexInitial == 0)
            {
                _initial[0] = source;
                DGridPlayers.ItemsSource = _allPlayers.Where(p => p.Player.Player.Name.StartsWith(source)).OrderBy(p => p.Player.ShirtNumber);
            }
            if (_indexInitial == 1)
            {
                _initial[1] = source;
                DGridPlayers.ItemsSource = _allPlayers.Where(p => p.Player.Player.Name.StartsWith(_initial[0]) && p.Player.Player.Name.Split()[1].StartsWith(_initial[1])).OrderBy(p => p.Player.ShirtNumber);
            }
            _indexInitial++;
        }
        private void comboTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentTeams = comboTeams.SelectedItem as Team;
            var currentPlayers = _allPlayers.Where(p => p.Player.Team.TeamName == currentTeams.TeamName).OrderBy(p => Convert.ToInt32(p.Player.ShirtNumber)).ToList();
            DGridPlayers.ItemsSource = currentPlayers.Take(_top10);
            _top10 = 10;
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentPlayers = _allPlayers.FindAll(p => p.Player.Player.Name.ToLower().Contains(Search.Text.ToLower()) && (p.Player.Team.TeamName == comboTeams.Text || string.IsNullOrEmpty(comboTeams.Text))).OrderBy(p => Convert.ToInt32(p.Player.ShirtNumber)).ToList();
            DGridPlayers.ItemsSource = currentPlayers.Take(_top10);
        }

        private void comboSeason_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSeason = comboSeason.SelectedItem as Season;
            var currentPlayers = _allPlayers.FindAll(p => p.Player.Player.Name.ToLower().Contains(Search.Text.ToLower()) && p.Player.Season.Name == currentSeason.Name).ToList();
            DGridPlayers.ItemsSource = currentPlayers.Take(_top10);
            _top10 = 10;
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (_top10 > 10)
            {
                _top10 -= 10;
                List<AnonimPlayer> currentPlayers = new List<AnonimPlayer>();
                for (int i = _top10 - 10; i < _top10; i++)
                    currentPlayers.Add(_allPlayers[i]);
                DGridPlayers.ItemsSource = currentPlayers;
                Position.Text = $"{_top10 / 10}";
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

            if (_top10 < _allPlayers.Where(p => p.Player.Team.TeamName.Contains(comboTeams.Text) && p.Player.Season.Name == comboSeason.Text).Count())
            {
                _top10 += 10;
                List<AnonimPlayer> currentPlayers = new List<AnonimPlayer>();
                for (int i = _top10 - 10; i < _top10; i++)
                    try { currentPlayers.Add(_allPlayers[i]); } catch { }
                DGridPlayers.ItemsSource = currentPlayers;
                Position.Text = $"{_top10 / 10}";
            }
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            _top10 = 10;
            List<AnonimPlayer> currentPlayers = new List<AnonimPlayer>();
            for (int i = _top10 - 10; i <= _top10; i++)
                currentPlayers.Add(_allPlayers[i]);
            DGridPlayers.ItemsSource = currentPlayers;
            Position.Text = $"1";
            DGridPlayers.ItemsSource = currentPlayers;
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {

            _top10 = _allPlayers.Count;
            List<AnonimPlayer> currentPlayers = new List<AnonimPlayer>();
            for (int i = _top10 - 10; i < _top10; i++)
                try { currentPlayers.Add(_allPlayers[i]); } catch { }
            DGridPlayers.ItemsSource = currentPlayers;

            Position.Text = $"{_allPlayers.Count / 10}";
            if (_allPlayers.Count % 10 > 0)
                ofPosition.Content = $"{_allPlayers.Count / 10 + 1}";
        }

        private void Position_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Convert.ToInt32(Position.Text);
            }
            catch
            {
                if (!string.IsNullOrEmpty(Position.Text))
                    MessageBox.Show("Введите число");
                return;
            }
            if (Convert.ToInt32(Position.Text) <= 0 || Convert.ToInt32(Position.Text) > Convert.ToInt32(ofPosition.Content.ToString().Replace("of", null)))
            {
                MessageBox.Show("Нет такой позиции");
                return;
            }
            else
            {
                List<AnonimPlayer> currentPlayers = new List<AnonimPlayer>();
                _top10 = Convert.ToInt32(Position.Text) * 10;
                for (int i = _top10 - 10; i < _top10; i++)
                    if (_allPlayers.Count >= _top10)
                        currentPlayers.Add(_allPlayers[i]);
                DGridPlayers.ItemsSource = currentPlayers;
            }
        }

        private void PlayerName_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PlayerDetail((sender as Button).DataContext as PlayerStatistics));
        }
    }
}
