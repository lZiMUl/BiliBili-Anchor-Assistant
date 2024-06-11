using com.lZiMUl.BiliBili_Anchor_Assistant.Enum;
using com.lZiMUl.BiliBili_Anchor_Assistant.Tools;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace com.lZiMUl.BiliBili_Anchor_Assistant.Views
{

    public partial class SongManager
    {

        private readonly List<Song> _songs;

        public SongManager()
        {
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            MainWindow.AddWindow(this);
            AddSongButton.Content = Config.LanguageResource.AddSong;
            _songs = Config.SongListConfigurationManagerHelper.LoadConfig();
            foreach (var song in _songs)
            {
                AddSongToUi(song.Uid, song);
            }
        }

        public void AddSong(SongMeta songMeta)
        {
            string guid = Guid.NewGuid().ToString();
            var song = new Song
            {
                Uid = guid,
                Name = songMeta.Name,
                Author = songMeta.Author,
                Time = songMeta.Time,
                Size = songMeta.Size
            };
            _songs.Add(song);
            Config.SongListConfigurationManagerHelper.SaveConfig(_songs);

            AddSongToUi(guid, song);
            new PlaySound().Play(SoundTypeEnum.WindowsHardwareInsert);
        }

        private void AddSongEvent(object sender, RoutedEventArgs e)
        {
            string guid = Guid.NewGuid().ToString();
            var song = new Song
            {
                Uid = guid,
                Name = "a",
                Author = "b",
                Time = "c",
                Size = "d"
            };

            _songs.Add(song);
            Config.SongListConfigurationManagerHelper.SaveConfig(_songs);

            AddSongToUi(guid, song);
            new PlaySound().Play(SoundTypeEnum.WindowsHardwareInsert);
        }

        private void DeleteSong(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Parent is Grid grid)
            {
                for (int index = 0; index < Config.SongListConfigurationManagerHelper.LoadConfig().Count; index++)
                {
                    if (Guid.Parse(grid.Children[0].GetValue(UidProperty).ToString() ?? new Guid().ToString()) == Guid.Parse(Config.SongListConfigurationManagerHelper.LoadConfig()[index].Uid ?? Guid.NewGuid().ToString()))
                    {
                        _songs.Remove(_songs[index]);
                        Config.SongListConfigurationManagerHelper.SaveConfig(_songs);
                        break;
                    }
                }
                SongList.Children.Remove(grid);
                new PlaySound().Play(SoundTypeEnum.WindowsHardwareRemove);
            }
        }

        private Label CreateLabel(string guid, string content, SolidColorBrush background) => new()
        {
            Uid = guid,
            Content = $"{content}",
            Width = 175,
            Height = 45,
            Margin = new Thickness(2, 1, 2, 1),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Background = background
        };

        private void AddSongToUi(string guid, Song song)
        {
            var grid = new Grid
            {
                Margin = new Thickness(0, 3, 0, 0)
            };
            var labels = new List<Label>
            {
                CreateLabel(guid, song.Name ?? "Error", Brushes.BurlyWood),
                CreateLabel(guid, song.Author ?? "Error", Brushes.BurlyWood),
                CreateLabel(guid, song.Time ?? "Error", Brushes.BurlyWood),
                CreateLabel(guid, song.Size ?? "Error", Brushes.BurlyWood)
            };
            var deleteButton = new Button
            {
                TabIndex = 5,
                Content = Config.LanguageResource.Delete,
                Width = 175,
                Height = 45,
                Background = Brushes.Tomato,
                Style = FindResource("ButtonStyle") as Style,
                Margin = new Thickness(2, 1, 2, 1),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };
            var star = new GridLength(1, GridUnitType.Star);
            labels.Select((value, index) => new { Index = index, Value = value })
                  .ToList()
                  .ForEach(item =>
                  {
                      grid.ColumnDefinitions.Add(new ColumnDefinition { Width = star });
                      Grid.SetColumn(item.Value, item.Value.TabIndex = +item.Index);
                      grid.Children.Add(item.Value);
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
        public class Song
        {
            public string? Uid { get; init; }
            public string? Name { get; init; }
            public string? Author { get; init; }
            public string? Time { get; init; }
            public string? Size { get; init; }
        }
        public class SongMeta
        {
            public string? Name { get; init; }
            public string? Author { get; init; }
            public string? Time { get; init; }
            public string? Size { get; init; }
        }
    }
}
