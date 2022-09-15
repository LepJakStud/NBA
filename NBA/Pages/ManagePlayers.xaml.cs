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
    /// Логика взаимодействия для ManagePlayers.xaml
    /// </summary>
    public partial class ManagePlayers : Page
    {
        public ManagePlayers()
        {
            InitializeComponent();
            DGridPlayers.ItemsSource = NBAEntities.GetContext().PlayerInTeam.ToList().Where(p=>p.SeasonId == 3).OrderBy(p=>p.Player.Name);
            List<string> country = new List<string>() { "All" };
            country.AddRange(NBAEntities.GetContext().Country.Select(p => p.CountryName).ToList());
            List<string> position = new List<string>() { "All" };
            position.AddRange(NBAEntities.GetContext().Position.Select(p => p.Name).ToList());
            Total.Content = Total.Content.ToString().Replace("XX", $"{NBAEntities.GetContext().PlayerInTeam.ToList().Where(p => p.SeasonId == 3).Count()}");
            comboCountry.ItemsSource = country.OrderBy(c=>c);
            comboCountry.SelectedIndex = 0;
            comboPosition.ItemsSource = position.OrderBy(p=>p);
            comboPosition.SelectedIndex = 0;
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (comboCountry.Text == "All" && comboPosition.Text == "All")
            {
                DGridPlayers.ItemsSource = NBAEntities.GetContext().PlayerInTeam.ToList().Where(p => p.SeasonId == 3 && p.Player.Name.ToLower().Contains(Name.Text.ToLower())).OrderBy(p => p.Player.Name);
                return;
            }
            if (comboCountry.Text == "All")
            {
                DGridPlayers.ItemsSource = NBAEntities.GetContext().PlayerInTeam.ToList().Where(p => p.SeasonId == 3 && p.Player.Name.ToLower().Contains(Name.Text.ToLower()) && p.Player.Position.Name == comboPosition.Text).OrderBy(p => p.Player.Name);
                return;
            }
            if (comboPosition.Text == "All")
            {
                DGridPlayers.ItemsSource = NBAEntities.GetContext().PlayerInTeam.ToList().Where(p => p.SeasonId == 3 && p.Player.Name.ToLower().Contains(Name.Text.ToLower()) && p.Player.Country.CountryName == comboCountry.Text).OrderBy(p => p.Player.Name);
                return;
            }
            DGridPlayers.ItemsSource = NBAEntities.GetContext().PlayerInTeam.ToList().Where(p => p.SeasonId == 3 && p.Player.Name.ToLower().Contains(Name.Text.ToLower()) && p.Player.Country.CountryName == comboCountry.Text && p.Player.Position.Name == comboPosition.Text).OrderBy(p => p.Player.Name);
        }
    }
}
