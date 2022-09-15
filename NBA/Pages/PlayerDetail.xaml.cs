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
    /// Логика взаимодействия для PlayerDetail.xaml
    /// </summary>
    public partial class PlayerDetail : Page
    {
        public PlayerDetail(PlayerStatistics selectedPlayer)
        {
            InitializeComponent();
            Name.Content = selectedPlayer.Player.Name;
            ShirtNumber.Content = $" | {selectedPlayer.Player.PlayerInTeam.Select(s => s.ShirtNumber)} | ";
            Team.Content = $"{selectedPlayer.Player.PlayerInTeam.Select(t=>t.Team.TeamName)}";
        }
    }
}
