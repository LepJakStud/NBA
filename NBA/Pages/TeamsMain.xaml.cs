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
    /// Логика взаимодействия для TeamsMain.xaml
    /// </summary>
    public partial class TeamsMain : Page
    {
        public TeamsMain()
        {
            InitializeComponent();
            
        }
        public void LoadData()
        {
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var esternDivision = App.DB.Conferences
                .FirstOrDefault(x => x.Name == "Eastern");
            var westernDivision = App.DB.Conferences
                .FirstOrDefault(x => x.Name == "Western");


            EasterDivision.ItemsSource = esternDivision.Divisions;
            WesternDivision.ItemsSource = westernDivision.Divisions;

        }

    }
}
