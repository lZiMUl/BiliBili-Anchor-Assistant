using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BiliBili_Anchor_Assistant.Views
{
    public partial class SongManager
    {
        public SongManager()
        {
            InitializeComponent();
            Icon = Config.Icon;
        }

        private void AddSong(object sender, RoutedEventArgs e)
        {
            var stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

            var nameTextBox = new TextBlock { Text = "a", Width = 160 };
            var authorTextBox = new TextBlock { Text = "b", Width = 160 };
            var timeTextBox = new TextBlock { Text = "c", Width = 160 };
            var sizeTextBox = new TextBlock { Text = "d", Width = 160 };

            var deleteButton = new Button
            {
                Content = "Delete",
                Background = Brushes.OrangeRed,
                Style = FindResource("ButtonStyle") as Style
            };
            deleteButton.Click += DeleteSong;

            stackPanel.Children.Add(nameTextBox);
            stackPanel.Children.Add(authorTextBox);
            stackPanel.Children.Add(timeTextBox);
            stackPanel.Children.Add(sizeTextBox);
            stackPanel.Children.Add(deleteButton);

            int rowIndex = SongList.RowDefinitions.Count;
            SongList.RowDefinitions.Add(new RowDefinition());
            Grid.SetRow(stackPanel, rowIndex + 1);
            SongList.Children.Add(stackPanel);
        }
        private void DeleteSong(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var stackPanel = button.Parent as StackPanel;

            // 从 SongList 中移除该 StackPanel
            SongList.Children.Remove(stackPanel);
        }
    }
}
