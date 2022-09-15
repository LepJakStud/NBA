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
    /// Логика взаимодействия для MatchupMain.xaml
    /// </summary>
    public partial class MatchupMain : Page
    {
        private static List<AnonimMatchup> _allMatchup =new List<AnonimMatchup>();
        public MatchupMain()
        {
            InitializeComponent();
            MainDate.Text = $"{DateTime.Now:d}";
            for (int i = 0; i < NBAEntities.GetContext().Matchup.ToList().Count; i++)
            {
                _allMatchup.Add(new AnonimMatchup(NBAEntities.GetContext().Matchup.ToList()[i]));
                if(!_allMatchup[i].homeTeam.Logo.Contains("\\Images\\Logo\\"))
                _allMatchup[i].homeTeam.Logo = _allMatchup[i].homeTeam.Logo.Insert(0, "\\Images\\Logo\\");
                if (!_allMatchup[i].awayTeam.Logo.Contains("\\Images\\Logo\\"))
                    _allMatchup[i].awayTeam.Logo = _allMatchup[i].awayTeam.Logo.Insert(0, "\\Images\\Logo\\");
            }
            DGridMatches.ItemsSource = _allMatchup;            
            LViewCurrentMatch.ItemsSource = new List<AnonimMatchup>() { _allMatchup.OrderBy(m => Convert.ToDateTime(MainDate.Text).Subtract(m.Matchup.Starttime)).ToList()[0] };
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            if((sender as Button).DataContext is AnonimMatchup status)  
               if( status.Status!= "Not Start")
                    Manager.MainFrame.Navigate(new MatchupDetail((sender as Button).DataContext as AnonimMatchup));
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            MainDate.Text = $"{Convert.ToDateTime(MainDate.Text).AddDays(-1):d}";
            LViewCurrentMatch.ItemsSource = new List<AnonimMatchup>() { _allMatchup.OrderBy(m => Convert.ToDateTime(MainDate.Text).Subtract(m.Matchup.Starttime)).ToList()[0] };
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            MainDate.Text = $"{Convert.ToDateTime(MainDate.Text).AddDays(1):d}";
            LViewCurrentMatch.ItemsSource = new List<AnonimMatchup>() { _allMatchup.OrderBy(m => Convert.ToDateTime(MainDate.Text).Subtract(m.Matchup.Starttime)).ToList()[0] };
        }
        private void MainDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //LViewCurrentMatch.ItemsSource = new List<AnonimMatchup>() { _allMatchup.OrderBy(m => Convert.ToDateTime(MainDate.SelectedDate).Subtract(m.Matchup.Starttime)).ToList()[0] };
        }
    }
}
