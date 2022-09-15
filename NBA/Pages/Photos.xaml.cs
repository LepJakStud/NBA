using System;
using System.Data;
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
using NBA.Models;
using Microsoft.Win32;
using System.Drawing.Imaging;

namespace NBA.Pages
{
    /// <summary>
    /// Логика взаимодействия для Photos.xaml
    /// </summary>
    public partial class Photos : Page
    {
        public static List<GamePhotos> _allPhotos =new List<GamePhotos>();
        public int _count = 
            Directory.GetFiles($@"{Directory.GetCurrentDirectory().Replace("\\bin\\Debug", null)}\Images\Pictures").Count() % 4 != 0 ?
            Directory.GetFiles($@"{Directory.GetCurrentDirectory().Replace("\\bin\\Debug", null)}\Images\Pictures").Count() / 3 : 
            Directory.GetFiles($@"{Directory.GetCurrentDirectory().Replace("\\bin\\Debug", null)}\Images\Pictures").Count() / 3 + 1;
        public int _top3 = 3;
        public Photos()
        {
            InitializeComponent();
            Total.Content = Total.Content.ToString().Replace("XX", $"{Directory.GetFiles($@"{Directory.GetCurrentDirectory().Replace("\\bin\\Debug", null)}\Images\Pictures").Count()}").Replace("YY", "12").Replace("ZZ", _count%3==0?$"{_count/3}": $"{_count / 3+1}");
            if(_allPhotos.Count == 0)
            for (int i = 0, j = 0; j <= _count; i += 4, j++)
                try
                {
                    _allPhotos.Add(new GamePhotos(
                        new BitmapImage(new Uri($"pack://application:,,,/images/Pictures/{1 + i}.jpg")),
                        new BitmapImage(new Uri($"pack://application:,,,/images/Pictures/{2 + i}.jpg")),
                        new BitmapImage(new Uri($"pack://application:,,,/images/Pictures/{3 + i}.jpg")),
                        new BitmapImage(new Uri($"pack://application:,,,/images/Pictures/{4 + i}.jpg"))));
                }
                catch
                {

                    try
                    {
                        _allPhotos.Add(new GamePhotos(
                            new BitmapImage(new Uri($"pack://application:,,,/images/Pictures/{1 + i}.jpg")),
                            new BitmapImage(new Uri($"pack://application:,,,/images/Pictures/{2 + i}.jpg")),
                            new BitmapImage(new Uri($"pack://application:,,,/images/Pictures/{3 + i}.jpg")), null));
                    }
                    catch
                    {

                        try
                        {
                            _allPhotos.Add(new GamePhotos(
                                new BitmapImage(new Uri($"pack://application:,,,/images/Pictures/{1 + i}.jpg")),
                                new BitmapImage(new Uri($"pack://application:,,,/images/Pictures/{2 + i}.jpg")),null, null));
                        }
                        catch
                        {
                            try
                            {
                                _allPhotos.Add(new GamePhotos(
                                    new BitmapImage(new Uri($"pack://application:,,,/images/Pictures/{1 + i}.jpg")),null, null, null));
                            }
                            catch { }
                        }
                    }
                }
            
            Position.Text = "1";
            DGridPhotos.ItemsSource = _allPhotos.Take(3);
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (_top3 > 3)
            {
                _top3 -= 3;
                List<GamePhotos> currentPhotos = new List<GamePhotos>();
                for (int i = _top3 - 3; i < _top3; i++)
                    currentPhotos.Add(_allPhotos[i]);
                DGridPhotos.ItemsSource = currentPhotos;
                Position.Text = $"{Convert.ToInt32(Position.Text) - 1}";
            }
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (_top3 < _count)
            {
                _top3 += 3;
                List<GamePhotos> currentPhotos = new List<GamePhotos>();
                for (int i = _top3 - 3; i < _top3; i++)
                    try { currentPhotos.Add(_allPhotos[i]); } catch { }
                Position.Text = $"{Convert.ToInt32(Position.Text) + 1}";
                DGridPhotos.ItemsSource = currentPhotos;
            }    
        }
        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            _top3 = 3;
            List<GamePhotos> currentPhotos = new List<GamePhotos>();
            for (int i = _top3 - 3; i < _top3; i++)
                try { currentPhotos.Add(_allPhotos[i]); } catch { }
            DGridPhotos.ItemsSource = currentPhotos;
            Position.Text = "1";
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            _top3 = _count;
            List<GamePhotos> currentPhotos = new List<GamePhotos>();
            for (int i = _top3 - 3; i < _top3; i++)
                try { currentPhotos.Add(_allPhotos[i]); } catch { }
            DGridPhotos.ItemsSource = currentPhotos;
            Position.Text = _allPhotos.Count % 3 == 0 ? $"{_allPhotos.Count / 3}" : $"{_allPhotos.Count / 3 + 1}";
        }

        private void Position_TextChanged(object sender, TextChangedEventArgs e)
        { 
            try
            {
                Convert.ToInt32(Position.Text);
            }
            catch
            {
                if (!string.IsNullOrEmpty(Position.Text))
                    MessageBox.Show("Введите число");
                return;
            }
            if (Convert.ToInt32(Position.Text) <= 0 || Convert.ToInt32(Position.Text) > (_count % 3 == 0 ? _count / 3 : _count / 3 + 1))
            {
                MessageBox.Show("Нет такой позиции");
                return;
            }
            else
            {
                _top3 = 3*Convert.ToInt32(Position.Text);
                List<GamePhotos> currentPhotos = new List<GamePhotos>();
                for (int i = _top3 - 3; i < _top3; i++)
                    try { currentPhotos.Add(_allPhotos[i]); } catch { }
                DGridPhotos.ItemsSource = currentPhotos;
            }
        }
        private void Download_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".jpg";
            save.Filter = "JPG Files (*.jpg)|*.jpg";
            save.FileName = "photo";
            JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
            jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(new Uri(_allPhotos[DGridPhotos.SelectedIndex].Photo1.ToString())));
            using (FileStream fs = new FileStream(save.SafeFileName, FileMode.Create))
            {
                jpegBitmapEncoder.Save(fs);
            }
            save.ShowDialog();
        }
    }
}