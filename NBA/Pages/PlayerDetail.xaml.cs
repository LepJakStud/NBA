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
using System.Windows.Forms.DataVisualization.Charting;

namespace NBA.Pages
{
    /// <summary>
    /// Логика взаимодействия для PlayerDetail.xaml
    /// </summary>
    public partial class PlayerDetail : Page
    {
        private Player Player = new Player();
        private string _lastSeasonName;

        public string LastSeasonName
        {
            get { return $"{_lastSeasonName} Season"; }
            set { _lastSeasonName = value; }
        }


        public void InitialChart()
        {
            var chartArea = new ChartArea("Main");

            chartArea.AxisX.LabelStyle.Format = $"MM'/'dd";
            ChartPoints.ChartAreas.Add(chartArea);
            ChartPoints.Titles.Add(new Title
            {
                Text = "Points",
                Name = "Points",
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F)
            });
            ChartPoints.Height = 250;
            var series = new Series("Points");
            ChartPoints.Series.Add(series);
        }
        public PlayerDetail()
        {
            InitializeComponent();

            Player = App.DB.Players.FirstOrDefault();
            DataContext = Player;
            LastSeasonName = App.DB.Matchups.OrderByDescending(x => x.Starttime).FirstOrDefault()?.Season.Name;
            InitialChart();
            
            LoadData();
        }
        public void LoadData()
        {
            var avgPoints = Player.PlayerStatistics.Average(x => x.Point);
            AvgOfPointsTextBlock.Text = $"The average of points: {Math.Round(avgPoints,2)}";
            LastSeason.Text = LastSeasonName;

            var series = ChartPoints.Series.FirstOrDefault();
            series?.Points.Clear();
            series.ChartType = SeriesChartType.Line;
            var data = Player?.PlayerStatistics.OrderBy(x => x.Matchup.Starttime);
            
            foreach (var item in data)
            {
                series.Points.AddXY(item.Matchup.Starttime, item.Point);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
