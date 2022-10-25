using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace NBA.Pages
{
    /// <summary>
    /// Логика взаимодействия для PlayersMain.xaml
    /// </summary>
    public partial class PlayersMain : Page
    {
        public PlayersMain()
        {
            InitializeComponent();
            LoadData();
        }
        public List<Player> Players { get; set; }
        public void LoadData()
        {
            var players = App.DB.Players.AsQueryable();
            if (TeamComboBox.SelectedIndex > 0
                && TeamComboBox.SelectedItem is Team team)
                players = players.Where(x => x.PlayerInTeams.Any(y => y.TeamId == team.TeamId));
            if (SeasonComboBox.SelectedIndex > 0
                && TeamComboBox.SelectedItem is Season season)
                players = players.Where(x => x.PlayerInTeams.Any(y => y.SeasonId == season.SeasonId));
            if (AlphabelFilterListView.SelectedIndex > 0
                && AlphabelFilterListView.SelectedItem is string startLetter)
                players = players.Where(x => x.Name.ToUpper().StartsWith(startLetter.ToUpper()));
            if (!string.IsNullOrWhiteSpace(PlayerNameTextBox.Text)
                && PlayerNameTextBox.Text.Length > 3)
                players = players.Where(x => x.Name.ToUpper().Contains(PlayerNameTextBox.Text.ToUpper()));

            Players = players.ToList();
            PlayersDataGrid.ItemsSource = Players;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                AlphabelFilterListView.ItemsSource = new string[] { "ALL", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "U", "W", "X", "Y", "Z" };
                var seasons = App.DB.Seasons.ToList();
                seasons.Insert(0, new Season()
                {
                    Name = "Все"
                });
                SeasonComboBox.ItemsSource = seasons;
                SeasonComboBox.DisplayMemberPath = nameof(Season.Name);

                var teams = App.DB.Teams.ToList();
                teams.Insert(0, new Team()
                {
                    TeamName = "Все"
                });

                TeamComboBox.ItemsSource = teams;
                TeamComboBox.DisplayMemberPath = nameof(Team.TeamName);

                LoadData();

            }
            catch (Exception ex)
            {

            }

        }

        private void SeasonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void TeamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void AlphabelFilterListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void PlayerNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData();
        }

        private void PlayersDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(PlayersDataGrid.SelectedItem is Player player)
            {
                var playerDetailPage = new PlayerDetail(player);
                Manager.MainFrame.Navigate(playerDetailPage);
            }
        }
    }
}
