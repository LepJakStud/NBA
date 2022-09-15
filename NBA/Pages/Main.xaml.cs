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

namespace NBA.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public List<BitmapImage> _Images = new List<BitmapImage>();
        public int[] _currentImages = new int[3] { 0, 1, 2 };
        public static SolidColorBrush color = new SolidColorBrush(Color.FromRgb(105, 149, 194));
        public Main()
        {
            InitializeComponent();
            Blue.Content = $"The current season is {DateTime.Now.Year - 1}-{DateTime.Now.Year}, and the NBA already has a history {DateTime.Now.Year - 1946} years";
            btnAdmin.Background = color;
            btnVisitor.Background = color;
        }

        private void btnVisitor_Click(object sender, RoutedEventArgs e)
        {
           
            Manager.MainFrame.Navigate(new VisitorMenu());
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AdminLogin());
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 1; i <= Directory.GetFiles($@"{Directory.GetCurrentDirectory().Replace("\\bin\\Debug", null)}\Images\Pictures").Count(); i++)
                    _Images.Add(new BitmapImage(new Uri($"pack://application:,,,/images/Pictures/{i}.jpg")));
                ImageHolder1.Source = _Images[_currentImages[0]];
                ImageHolder2.Source = _Images[_currentImages[1]];
                ImageHolder3.Source = _Images[_currentImages[2]];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPreviousImage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentImages[0] - 3 < 0)
            {
                _currentImages[0] = _Images.Count - 3;
                _currentImages[1] = _Images.Count - 2;
                _currentImages[2] = _Images.Count - 1;
            }
            else
            {
                _currentImages[0] -= 3;
                _currentImages[1] -= 3;
                _currentImages[2] -= 3;
            }
            ImageHolder1.Source = _Images[_currentImages[0]];
            ImageHolder2.Source = _Images[_currentImages[1]];
            ImageHolder3.Source = _Images[_currentImages[2]];
        }

        private void btnNextImage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentImages[2] + 3 >= _Images.Count())
            {
                _currentImages[0] = 0;
                _currentImages[1] = 1;
                _currentImages[2] = 2;
            }
            else
            {
                _currentImages[0] += 3;
                _currentImages[1] += 3;
                _currentImages[2] += 3;
            }
            ImageHolder1.Source = _Images[_currentImages[0]];
            ImageHolder2.Source = _Images[_currentImages[1]];
            ImageHolder3.Source = _Images[_currentImages[2]];

        }
    }
}

