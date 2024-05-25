using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BiliBili_Anchor_Assistant.Views
{
    public partial class SongManager
    {
        public SongManager()
        {
            Icon = Config.Icon;
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            MainWindow.AddWindow(this);
        }

        private void AddSong(object sender, RoutedEventArgs e)
        {
            var grid = new Grid
            {
                Margin = new Thickness(0, 3, 0, 0)
            };
            var labels = new List<Label>
            {
                new()
                {
                    TabIndex = 0,
                    Content = "a", Width = 175,
                    Height = 45,
                    Margin = new Thickness(2, 1, 2, 1),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Background = Brushes.Aqua
                },
                new()
                {
                    TabIndex = 1,
                    Content = "b", Width = 175,
                    Height = 45,
                    Margin = new Thickness(2, 1, 2, 1),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Background = Brushes.BurlyWood
                },
                new()
                {
                    TabIndex = 2,
                    Content = "c", Width = 175,
                    Height = 45,
                    Margin = new Thickness(2, 1, 2, 1),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Background = Brushes.DarkGreen
                },
                new()
                {
                    TabIndex = 3,
                    Content = "d", Width = 175,
                    Height = 45,
                    Margin = new Thickness(2, 1, 2, 1),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Background = Brushes.Gold
                }
            };
            var deleteButton = new Button
            {
                TabIndex = 5,
                Content = "Delete",
                Width = 175,
                Height = 45,
                Background = Brushes.Tomato,
                Style = FindResource("ButtonStyle") as Style,
                Margin = new Thickness(2, 1, 2, 1),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };
            var star = new GridLength(1, GridUnitType.Star);
            labels.ForEach(label =>
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = star });
                    Grid.SetColumn(label, (int)label.GetValue(TabIndexProperty));
                    grid.Children.Add(label);
            });

            deleteButton.Click += DeleteSong;
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = star });
            Grid.SetColumn(deleteButton, labels.Count);
            grid.Children.Add(deleteButton);

            int rowIndex = SongList.RowDefinitions.Count;
            SongList.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            Grid.SetRow(grid, rowIndex);
            SongList.Children.Add(grid);
        }

        private void DeleteSong(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Parent is Grid grid)
            {
                SongList.Children.Remove(grid);
            }
        }
    }
}