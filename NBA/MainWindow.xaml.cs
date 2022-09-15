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
using System.IO;
using NBA.Pages;

namespace NBA
{
    //    / <summary>
    //    / Логика взаимодействия для MainWindow.xaml
    //    / </summary>
    public partial class MainWindow : Window
    {
        public static Frame frame { get; set; } 

        public List<BitmapImage> _Images = new List<BitmapImage>();
        public int[] _currentImages = new int[3] { 0, 1, 2 };

        public MainWindow()
        {
            InitializeComponent();
            Blue.Content = $"The current season is {DateTime.Now.Year - 1}-{DateTime.Now.Year}, and the NBA already has a history {DateTime.Now.Year - 1946} years";
            Blue.Background = Main.color;
            Tools.BorderBrush = Main.color;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
           
           if(!Manager.MainFrame.CanGoBack)
           {
                Tools.Visibility = Visibility.Hidden;
                MainFrame.Margin = new Thickness(0, 0, 0, 0);
           }

           else
                Manager.MainFrame.GoBack();
        }
        //изменил код после добавления кнопки logout т.к. Main page слетал 
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is Main page)
            {
                Tools.Visibility= Visibility.Hidden;
                MainFrame.Margin = new Thickness(0);
                return;
            }
            else
            {
                Tools.Visibility = Visibility.Visible;
                MainFrame.Margin = new Thickness(0, 70, 0, 0);
            }
            if (e.Content is EventAdministratorMenu || e.Content is TechnicalAdministratorMenu || e.Content is ManageSeason || e.Content is ManageTeams || e.Content is ManagePlayers || e.Content is ManageMatchups)
                btnLogout.Visibility = Visibility.Visible;
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Main());
            Manager.MainFrame = MainFrame;
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if(Manager.MainFrame.CanGoBack && 
                !(Manager.MainFrame.Content is Main))
            {
                Tools.Visibility = Visibility.Visible;
                MainFrame.Margin = new Thickness(0, 70, 0, 0);
            }
            else
            {
                Tools.Visibility = Visibility.Hidden;
                MainFrame.Margin = new Thickness(0, 0, 0, 0);
            }
            //if(e.Content is Main || e.Content is VisitorMenu)


        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            while(Manager.MainFrame.CanGoBack)
                Manager.MainFrame.GoBack();
            btnBack_Click(null, null);
            btnLogout.Visibility=Visibility.Hidden;
        }
    }
}
