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
    /// Логика взаимодействия для TechnicalAdministratorMenu.xaml
    /// </summary>
    public partial class TechnicalAdministratorMenu : Page
    {
        public TechnicalAdministratorMenu()
        {
            InitializeComponent();
        }

        private void btnExecutions_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The feature would be a future add-on to the current system", "Manage Executions – Future Add-on");

        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TeamReport());
        }
    }
}
