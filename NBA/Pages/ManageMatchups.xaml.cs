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
    /// Логика взаимодействия для ManageMatchups.xaml
    /// </summary>
    public partial class ManageMatchups : Page
    {
        public static Season _currentSeason = new Season();
        private List<AnonimMatchup> _allMatchups = new List<AnonimMatchup>();
        public ManageMatchups(int season,DateTime dateTime, bool? check)
        {
            InitializeComponent();
                int count = _allMatchups.Count();
            if (_allMatchups.Count + 1 != NBAEntities.GetContext().Matchup.ToList().Count)
            for (int i = count; i<NBAEntities.GetContext().Matchup.ToList().Count; i++)
            {
                _allMatchups.Add(new AnonimMatchup(NBAEntities.GetContext().Matchup.ToList()[i]));
                _allMatchups[i].Status = NBAEntities.GetContext().Matchup.ToList()[i].Status == 1 ? "Yes" : "No";
            }
            comboSeason.ItemsSource = NBAEntities.GetContext().Season.ToList();
                comboSeason.SelectedIndex = season;
            DGridPreseaonMatches.ItemsSource = _allMatchups.Where(m => m.Matchup.MatchupTypeId == 0 && m.Matchup.Season.Name==comboSeason.Text).OrderBy(m => m.Matchup.Starttime);
            DGridRegularMatches.ItemsSource = _allMatchups.Where(m => m.Matchup.MatchupTypeId == 1 && m.Matchup.Season.Name == comboSeason.Text).OrderBy(m => m.Matchup.Starttime);
            Date.IsChecked = check;
            MainDate.DisplayDate = dateTime;
            //Возвращает только первое счисло месяца
            if(Date.IsChecked==true)
            {
                DGridPreseaonMatches.ItemsSource = _allMatchups.Where(m => m.Matchup.MatchupTypeId == 0 && m.Matchup.Season.Name == comboSeason.Text && m.Matchup.Starttime.DayOfYear == dateTime.DayOfYear).OrderBy(m=>m.Matchup.Starttime);
                DGridRegularMatches.ItemsSource = _allMatchups.Where(m => m.Matchup.MatchupTypeId == 1 && m.Matchup.Season.Name == comboSeason.Text && m.Matchup.Starttime.DayOfYear == dateTime.DayOfYear).OrderBy(m => m.Matchup.Starttime);
                MessageBox.Show(dateTime.Day.ToString());
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var matchupForRemoving = DGridRegularMatches.SelectedItems.Cast<AnonimMatchup>().FirstOrDefault();
            if(MessageBox.Show("Do you really want to delete the next match",
                "Warning", MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                try
                {
                    NBAEntities.GetContext().Matchup.Remove(NBAEntities.GetContext().Matchup.ToList().Where(m=>m.MatchupId==matchupForRemoving.Matchup.MatchupId).FirstOrDefault());
                    NBAEntities.GetContext().SaveChanges();
                    MessageBox.Show("Match deleted");
                }
                catch
                {
                    MessageBox.Show("Error while deleting");
                }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The  feature would be a future add-on to the current system", "Add a new team – Future Add-on");
        }

        private void btnDelete_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You cant delete a completed match");
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ManageMatchups(comboSeason.SelectedIndex, MainDate.DisplayDate, Date.IsChecked));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddMatchup());
        }

    }
}
