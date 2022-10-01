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

namespace NBA.Pages
{
    /// <summary>
    /// Логика взаимодействия для PlayerDetail.xaml
    /// </summary>
    public partial class PlayerDetail : Page
    {
        private Player Player = new Player();

        public PlayerDetail()
        {
            InitializeComponent();

            DataContext = App.DB.Players.FirstOrDefault();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
