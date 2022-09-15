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
    /// Логика взаимодействия для MatchupDetail.xaml
    /// </summary>
    public partial class MatchupDetail : Page
    {
        public static List<AnonimMatchup> _currentMatch;
        public MatchupDetail(AnonimMatchup selectedMatch)
        {
            InitializeComponent();
            if (selectedMatch != null)
                _currentMatch = new List<AnonimMatchup> { selectedMatch };
            LViewCurrentMatch.ItemsSource = _currentMatch;
            var list = new[] { new { Matchup = selectedMatch,
                Abbr= _currentMatch.Select(m=>m.awayTeam.Abbr).First(),
            Score1 = NBAEntities.GetContext().MatchupDetail.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.Quarter == 1).Select(s => s.Team_Away_Score).First(),
            Score2 = NBAEntities.GetContext().MatchupDetail.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.Quarter == 2).Select(s => s.Team_Away_Score).First(),
            Score3 = NBAEntities.GetContext().MatchupDetail.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.Quarter == 3).Select(s => s.Team_Away_Score).First(),
            Score4 = NBAEntities.GetContext().MatchupDetail.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.Quarter == 4).Select(s => s.Team_Away_Score).First(),
            Total = selectedMatch.Matchup.Team_Away_Score},
            new { Matchup = selectedMatch,
                Abbr= _currentMatch.Select(m=>m.homeTeam.Abbr).First(),
            Score1 = NBAEntities.GetContext().MatchupDetail.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.Quarter == 1).Select(s => s.Team_Home_Score).First(),
            Score2 = NBAEntities.GetContext().MatchupDetail.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.Quarter == 2).Select(s => s.Team_Home_Score).First(),
            Score3 = NBAEntities.GetContext().MatchupDetail.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.Quarter == 3).Select(s => s.Team_Home_Score).First(),
            Score4 = NBAEntities.GetContext().MatchupDetail.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.Quarter == 4).Select(s => s.Team_Home_Score).First(),
            Total = selectedMatch.Matchup.Team_Home_Score} };
            DGridScore.ItemsSource = list;
            LViewCurrentMatch.ItemsSource = _currentMatch;
            Status.Content= selectedMatch.Status;
            Status.Background = selectedMatch.color;
            //В базу введены не все значения

            //statistic
            var statistic = new[] {
                new{ homeLogo = selectedMatch.homeTeam.Logo, awayLogo = selectedMatch.awayTeam.Logo, action = "FG Made-Attempted",
                awayTeam =$"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.awayTeam.TeamId&& m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[2].ActionTypeId).Select(m=>m.ActionType).Count()}-{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.awayTeam.TeamId&& m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[3].ActionTypeId).Select(m=>m.ActionType).Count()}",
                homeTeam = $"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.homeTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[2].ActionTypeId).Select(m=>m.ActionType).Count()}-{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.homeTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[3].ActionTypeId).Select(m=>m.ActionType).Count()}"},
                new{ homeLogo = selectedMatch.homeTeam.Logo, awayLogo = selectedMatch.awayTeam.Logo, action = "3PT Made-Attempted",
                awayTeam =$"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.awayTeam.TeamId&& m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[4].ActionTypeId).Select(m=>m.ActionType).Count()}-{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.awayTeam.TeamId&& m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[5].ActionTypeId).Select(m=>m.ActionType).Count()}",
                homeTeam = $"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.homeTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[4].ActionTypeId).Select(m=>m.ActionType).Count()}-{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.homeTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[5].ActionTypeId).Select(m=>m.ActionType).Count()}"},
                new{ homeLogo = selectedMatch.homeTeam.Logo, awayLogo = selectedMatch.awayTeam.Logo, action = "FT Made-Attempted",
                awayTeam =$"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.awayTeam.TeamId&& m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[6].ActionTypeId).Select(m=>m.ActionType).Count()}-{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.awayTeam.TeamId&& m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[7].ActionTypeId).Select(m=>m.ActionType).Count()}",
                homeTeam = $"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.homeTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[6].ActionTypeId).Select(m=>m.ActionType).Count()}-{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.homeTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[7].ActionTypeId).Select(m=>m.ActionType).Count()}"},
                new{ homeLogo = selectedMatch.homeTeam.Logo, awayLogo = selectedMatch.awayTeam.Logo, action = "Rebounds",
                awayTeam =$"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.awayTeam.TeamId&& m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[8].ActionTypeId).Select(m=>m.ActionType).Count()+NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.awayTeam.TeamId&& m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[9].ActionTypeId).Select(m=>m.ActionType).Count()}",
                homeTeam = $"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.homeTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[8].ActionTypeId).Select(m=>m.ActionType).Count()+NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId==selectedMatch.homeTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[9].ActionTypeId).Select(m=>m.ActionType).Count()}"},
                new{homeLogo = selectedMatch.homeTeam.Logo, awayLogo = selectedMatch.awayTeam.Logo, action = "Assists",
                awayTeam = $"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[10].ActionTypeId).Select(m => m.ActionType).Count()}",
                homeTeam = $"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[10].ActionTypeId).Select(m => m.ActionType).Count()}"},
                new{homeLogo = selectedMatch.homeTeam.Logo, awayLogo = selectedMatch.awayTeam.Logo, action = "Steals",
                awayTeam = $"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[11].ActionTypeId).Select(m => m.ActionType).Count()}",
                homeTeam = $"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[11].ActionTypeId).Select(m => m.ActionType).Count()}"},
                new{homeLogo = selectedMatch.homeTeam.Logo, awayLogo = selectedMatch.awayTeam.Logo, action = "Blocks",
                awayTeam = $"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[12].ActionTypeId).Select(m => m.ActionType).Count()}",
                homeTeam = $"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[12].ActionTypeId).Select(m => m.ActionType).Count()}"},
                new{homeLogo = selectedMatch.homeTeam.Logo, awayLogo = selectedMatch.awayTeam.Logo, action = "Turnovers",
                awayTeam = $"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[13].ActionTypeId).Select(m => m.ActionType).Count()}",
                homeTeam = $"{NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionTypeId == NBAEntities.GetContext().ActionType.ToList()[13].ActionTypeId).Select(m => m.ActionType).Count()}"},};
           
            AwayHeader.Source = new BitmapImage(new Uri(selectedMatch.awayTeam.Logo, UriKind.Relative));
            HomeHeader.Source = new BitmapImage(new Uri(selectedMatch.homeTeam.Logo, UriKind.Relative));

            AwayTeamAbbr.Content = selectedMatch.awayTeam.Abbr;
            if (Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[2].Name).Select(m => m.ActionType).Count()) * 100 != 0)
                FieldAway.Width = 220 / NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name.Contains("Field Goal Made")).Select(m => m.ActionType).Count()
            * (NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name.Contains("Field Goal Made")).Select(m => m.ActionType).Count() -
            NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name.Contains("Field Goal Missed")).Select(m => m.ActionType).Count());
            if (Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[2].Name).Select(m => m.ActionType).Count()) * 100 != 0)
                FieldAwayPercent.Content = 100 - (Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[3].Name).Select(m => m.ActionType).Count())
                    / Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[2].Name).Select(m => m.ActionType).Count())) * 100;

            HomeTeamAbbr.Content = selectedMatch.homeTeam.Abbr;
            if (Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[2].Name).Select(m => m.ActionType).Count()) * 100 != 0)
                FieldHome.Width = 220 / NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name.Contains("Field Goal Made")).Select(m => m.ActionType).Count()
            * (NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name.Contains("Field Goal Made")).Select(m => m.ActionType).Count()
            - NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name.Contains("Field Goal Missed")).Select(m => m.ActionType).Count());
            if (Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[2].Name).Select(m => m.ActionType).Count()) * 100 != 0)
                FieldHomePercent.Content = 100 - (Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[3].Name).Select(m => m.ActionType).Count())
                    / Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[2].Name).Select(m => m.ActionType).Count())) * 100;


            AwayTeamAbbr2.Content = selectedMatch.awayTeam.Abbr;
            if (Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[5].Name).Select(m => m.ActionType).Count()) * 100 != 0)
                ThreePointAway.Width = 220 / NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name.Contains("3-Points Field Goal Made")).Select(m => m.ActionType).Count()
            * (NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name.Contains("3-Points Field Goal Made")).Select(m => m.ActionType).Count() -
            NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name.Contains("3-Points Field Goal Missed")).Select(m => m.ActionType).Count());
            if (Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[5].Name).Select(m => m.ActionType).Count()) * 100 != 0)
                ThreePointAwayPercent.Content = 100 - (Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[4].Name).Select(m => m.ActionType).Count())
                    / Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.awayTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[5].Name).Select(m => m.ActionType).Count())) * 100;

            HomeTeamAbbr2.Content = selectedMatch.homeTeam.Abbr;
            if (Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[4].Name).Select(m => m.ActionType).Count()) * 100 != 0)
                ThreePointHome.Width = 220 / NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name.Contains("3-Points Field Goal Made")).Select(m => m.ActionType).Count()
            * (NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name.Contains("3-Points Field Goal Made")).Select(m => m.ActionType).Count()
            - NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name.Contains("3-Points Field Goal Missed")).Select(m => m.ActionType).Count());
            if (Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[4].Name).Select(m => m.ActionType).Count()) * 100 != 0)
                ThreePointHomePercent.Content = 100 - (Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[5].Name).Select(m => m.ActionType).Count()) /
                    Convert.ToDouble(NBAEntities.GetContext().MatchupLog.ToList().Where(m => m.MatchupId == selectedMatch.Matchup.MatchupId && m.TeamId == selectedMatch.homeTeam.TeamId && m.ActionType.Name == NBAEntities.GetContext().ActionType.ToList()[4].Name).Select(m => m.ActionType).Count()) * 100);

            //starter
            DGridStates.ItemsSource = statistic;
            LViewStarterAwayTeam.ItemsSource = selectedMatch.awayTeam.PlayerInTeam.Where(p => p.StarterIndex == 1 && p.TeamId == selectedMatch.awayTeam.TeamId && p.SeasonId == selectedMatch.Matchup.SeasonId);
            LViewStarterHomeTeam.ItemsSource = selectedMatch.homeTeam.PlayerInTeam.Where(p => p.StarterIndex == 1 && p.TeamId == selectedMatch.homeTeam.TeamId && p.SeasonId == selectedMatch.Matchup.SeasonId);
            AwayLogo.Source = new BitmapImage(new Uri(selectedMatch.awayTeam.Logo, UriKind.Relative));
            HomeLogo.Source = new BitmapImage(new Uri(selectedMatch.homeTeam.Logo, UriKind.Relative));
            MainHomeLogo.Source = new BitmapImage(new Uri(selectedMatch.homeTeam.Logo, UriKind.Relative));

            //Log
            List<string> quarterList = new List<string>() { "1st", "2nd", "3rd", "4th" };
            comboQuarter.ItemsSource = quarterList;
            comboQuarter.SelectedIndex = 0;
            DGridLog.ItemsSource = NBAEntities.GetContext().MatchupLog.ToList().Where(q => q.Quarter == comboQuarter.SelectedIndex + 1 && q.MatchupId == selectedMatch.Matchup.MatchupId);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DGridLog.ItemsSource = NBAEntities.GetContext().MatchupLog.ToList().Where(q => q.Quarter == comboQuarter.SelectedIndex + 1 && q.MatchupId == _currentMatch[0].Matchup.MatchupId);
        }
    }
}