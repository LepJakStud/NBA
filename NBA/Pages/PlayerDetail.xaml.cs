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
        private Season LastSeason { get; set; }

        public PlayerDetail(Player player) : this()
        {
            Player = player;
            DataContext = player;
            LastSeason = App.DB.Matchups
                .OrderByDescending(x => x.Starttime)
                .FirstOrDefault()?.Season;
           
            LoadData();
        }

        public PlayerDetail()
        {
            InitializeComponent();            
            InitialChart();
            FilterListView.SelectedIndex = 1;
        }

        public void InitialChart()
        {
            var chartArea = new ChartArea("Main");

            chartArea.AxisX.LabelStyle.Format = $"MM'/'dd";
            ChartPoints.ChartAreas.Add(chartArea);
            chartArea.AxisX.Interval = 200;
            ChangeTitle("");
            ChartPoints.Height = 250;
            var series = new Series("Points");
            ChartPoints.Series.Add(series);
        }


        public void LoadData()
        {
            AvgOfPointsTextBlock.Text = $"The average of points: {Math.Round(Player.PPG, 2)}";
            LastSeasonName.Text = $"{LastSeason.Name} Season";

            var data = Player?.PlayerStatistics.OrderBy(x => x.Matchup.Starttime);
            var resultList = data.Select(x => new Tuple<int, DateTime>(x.Point, x.Matchup.Starttime)).ToList();
            FillChart(resultList);
        }

        public void FillChart(IEnumerable<Tuple<int, DateTime>> points)
        {
            var series = ChartPoints.Series.FirstOrDefault();
            series?.Points.Clear();
            series.ChartType = SeriesChartType.Line;

            foreach (var point in points)
            {
                series.Points.AddXY(point.Item2, point.Item1);
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var points = SelectPointsData(Player.PlayerStatistics).ToList();
            FillChart(points);

        }
        public IEnumerable<Tuple<int, DateTime>> SelectPointsData(IEnumerable<PlayerStatistic> statsList)
        {
            var fromDate = FromDatePicker?.SelectedDate;
            var toDate = ToDatePicker?.SelectedDate;
            if (fromDate != null)
                statsList = statsList
                    .Where(x => x.Matchup.Starttime >= fromDate);
            if (toDate != null)
                statsList = statsList
                    .Where(x => x.Matchup.Starttime <= toDate);

            statsList = statsList.OrderBy(x => x.Matchup.Starttime);
            var selectItem = FilterListView.SelectedItem.ToString();
            ChangeTitle(selectItem);
            switch (selectItem)
            {
                case "POINTS":
                    return statsList
                        .Select(x => new Tuple<int, DateTime>(x.Point, x.Matchup.Starttime));
                case "REBOUND":
                    return statsList
                        .Select(x => new Tuple<int, DateTime>(x.Rebound, x.Matchup.Starttime));
                case "ASSISTS":
                    return statsList
                        .Select(x => new Tuple<int, DateTime>(x.Assist, x.Matchup.Starttime));
                case "STEALS":
                    return statsList
                        .Select(x => new Tuple<int, DateTime>(x.Steal, x.Matchup.Starttime));
                case "BLOCKS":
                    return statsList
                        .Select(x => new Tuple<int, DateTime>(x.Block, x.Matchup.Starttime));
                default:
                    return statsList
                        .Select(x => new Tuple<int, DateTime>(x.Point, x.Matchup.Starttime));
            }
        }


        public void FillChart()
        {
            var series = ChartPoints.Series.FirstOrDefault();
            series?.Points.Clear();
            series.ChartType = SeriesChartType.Line;
            var data = Player?.PlayerStatistics.OrderBy(x => x.Matchup.Starttime);

            foreach (var item in data)
            {
                series.Points.AddXY(item.Matchup.Starttime, item.Point);
            }
        }

        private void FilterListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var points = SelectPointsData(Player.PlayerStatistics).ToList();
            FillChart(points);
        }
        public void ChangeTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                title = "POINTS";
            ChartPoints.Titles.Clear();
            ChartPoints.Titles.Add(new Title
            {
                Text = title,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F)
            });
        }
    }
}
