using System.Windows;
using System.Windows.Controls;

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

            var textBox = new TextBox();
            textBox.Text = "1";

            stackPanel.Children.Add(textBox);
            SongList.Children.Add(stackPanel);
        }
        private void DeleteSong(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
