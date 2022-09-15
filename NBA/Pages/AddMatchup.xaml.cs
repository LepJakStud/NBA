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
    /// Логика взаимодействия для AddMatchup.xaml
    /// </summary>
    public partial class AddMatchup : Page
    {
        public static Matchup _currentMatch = new Matchup();
        public AddMatchup()
        {
            InitializeComponent();

            DataContext = _currentMatch;
            Season.Content = $"Season: {NBAEntities.GetContext().Season.ToList().Last().Name}";
            comboAwayTeams.ItemsSource = NBAEntities.GetContext().Team.ToList();
            comboHomeTeams.ItemsSource = NBAEntities.GetContext().Team.ToList();
            MainDate.DisplayDateStart = Convert.ToDateTime("25.10.2016");
            MainDate.DisplayDateEnd = Convert.ToDateTime("17.06.2017");
            _currentMatch.MatchupTypeId = 1;
            _currentMatch.Status = -1;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                _currentMatch.Starttime = Convert.ToDateTime(MainDate.Text);
                _currentMatch.Starttime = _currentMatch.Starttime.AddHours(Convert.ToInt32(Time.Text.Remove(Time.Text.IndexOf(":"))));
                _currentMatch.Starttime = _currentMatch.Starttime.AddMinutes(Convert.ToInt32(Time.Text.Remove(0, Time.Text.IndexOf(":")+1)));
            }
            catch 
            {
                MessageBox.Show("Неверный формат даты и времени");
                return; 
            }

            if (_currentMatch.Starttime.Year > 2017)
                sb.AppendLine($"Date out of season");
            if (comboAwayTeams.SelectedIndex == comboHomeTeams.SelectedIndex)
                sb.AppendLine("Teams cant be the same");
            if (string.IsNullOrWhiteSpace(comboAwayTeams.Text) || string.IsNullOrEmpty(comboAwayTeams.Text) && string.IsNullOrWhiteSpace(comboHomeTeams.Text) || string.IsNullOrEmpty(comboHomeTeams.Text))
                sb.AppendLine("Выберите команды");
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return;
            }
            _currentMatch.SeasonId = NBAEntities.GetContext().Season.ToList().Last().SeasonId;
            _currentMatch.Team_Away = comboAwayTeams.SelectedIndex+1;
            _currentMatch.Team_Home = comboHomeTeams.SelectedIndex+1;
            _currentMatch.Team_Away_Score = 0;
            _currentMatch.Team_Home_Score = 0;
            if (string.IsNullOrWhiteSpace(_currentMatch.Location) || string.IsNullOrEmpty(_currentMatch.Location))
                _currentMatch.Location = NBAEntities.GetContext().Team.ToList().Where(t => t.TeamId == _currentMatch.Team_Home).Select(t => t.Stadium).FirstOrDefault();
            if (_currentMatch.MatchupId == 0)
                try
                {
                    NBAEntities.GetContext().Matchup.Add(_currentMatch);
                    NBAEntities.GetContext().SaveChanges();
                    MessageBox.Show("Data saved successfully");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
        }
    }
}