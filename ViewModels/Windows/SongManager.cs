using com.lZiMUl.BiliBili_Anchor_Assistant.Enum;
using com.lZiMUl.BiliBili_Anchor_Assistant.Tools;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace com.lZiMUl.BiliBili_Anchor_Assistant.ViewModels.Windows
{

    public class Panel
    {
        public Label TitleLabel { get; set; }
        public Label AuthorLabel { get; set; }
        public Label TimeLabel { get; set; }
        public Label SizeLabel { get; set; }
        public Button ActionButton { get; set; }
    }

    public partial class SongManager
    {
        private readonly List<Song> _songs;

        public SongManager()
        {
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            MainWindow.AddWindow(this);

            AddSongButton.Content = Config.LanguageResource.AddSong;
            Title.Header = Config.LanguageResource.Title;
            Author.Header = Config.LanguageResource.Author;
            Time.Header = Config.LanguageResource.Time;
            Size.Header = Config.LanguageResource.Size;
            Action.Header = Config.LanguageResource.Action;

            SongListCollection = new ObservableCollection<Grid>();
            SongListView.ItemsSource = SongListCollection;

            _songs = Config.SongListConfigurationManagerService.LoadConfig();
            foreach (var song in _songs)
            {
                AddSongToUi(song.Guid, song);
            }
        }

        public ObservableCollection<Grid> SongListCollection { get; set; }

        public void AddSong(SongMeta songMeta)
        {
            string guid = Guid.NewGuid().ToString();
            var song = new Song
            {
                Guid = guid,
                Name = songMeta.Name,
                Author = songMeta.Author,
                Time = songMeta.Time,
                Size = songMeta.Size
            };
            _songs.Add(song);
            Config.SongListConfigurationManagerService.SaveConfig(_songs);

            AddSongToUi(guid, song);
            new PlaySound().Play(SoundTypeEnum.WindowsHardwareInsert);
        }

        private void AddSongEvent(object sender, RoutedEventArgs e)
        {
            string guid = Guid.NewGuid().ToString();
            var song = new Song
            {
                Guid = guid,
                Name = "测试(Test)",
                Author = "lZiMUl",
                Time = DateTime.Now.ToString("h:mm:ss"),
                Size = "114514TB"
            };

            _songs.Add(song);
            Config.SongListConfigurationManagerService.SaveConfig(_songs);

            AddSongToUi(guid, song);
            new PlaySound().Play(SoundTypeEnum.WindowsHardwareInsert);
        }

        private void DeleteSong(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Parent is Grid grid)
            {
                for (int index = 0; index < Config.SongListConfigurationManagerService.LoadConfig().Count; index++)
                {
                    if (Guid.Parse(grid.Children[0].GetValue(UidProperty).ToString() ?? new Guid().ToString()) == Guid.Parse(Config.SongListConfigurationManagerService.LoadConfig()[index].Guid))
                    {
                        SongListCollection.Remove(grid);
                        _songs.Remove(_songs[index]);
                        Config.SongListConfigurationManagerService.SaveConfig(_songs);
                        break;
                    }
                }
                new PlaySound().Play(SoundTypeEnum.WindowsHardwareRemove);
            }
        }

        private Label CreateLabel(string guid, string content) => new()
        {
            Uid = guid,
            Content = content,
            Width = 140,
            Height = 45,
            Margin = new Thickness(1, -2, 1, 2),
            HorizontalContentAlignment = HorizontalAlignment.Left,
            VerticalContentAlignment = VerticalAlignment.Center,
            FontSize = 15,
            Background =  Brushes.BurlyWood

        };

        private void AddSongToUi(string guid, Song song)
        {
            var grid = new Grid
            {
                Margin = new Thickness(0, 3, 0, 0)
            };
            var labels = new List<Panel>
            {
                new()
                {
                    TitleLabel = CreateLabel(guid, song.Name),
                    AuthorLabel = CreateLabel(guid, song.Author),
                    TimeLabel = CreateLabel(guid, song.Time),
                    SizeLabel = CreateLabel(guid, song.Size),
                    ActionButton = new Button
                    {
                        TabIndex = 5,
                        Content = Config.LanguageResource.Delete,
                        Width = 140,
                        Height = 45,
                        Background = Brushes.Tomato,
                        Style = FindResource("ButtonStyle") as Style,
                        Margin = new Thickness(1, -2, 1, 2),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Top,
                        FontSize = 15
                    }
                }
            };
            foreach (var rawItem in labels)
            {
                var classType = typeof(Panel);
                classType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                         .Select((value, index) => new { Value = value, Index = index })
                         .ToList().ForEach(item =>
                         {
                             grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                             object propertyValue = classType.GetProperty(item.Value.Name)?.GetValue(rawItem);

                             switch (propertyValue)
                             {
                                 case Label label:
                                     Grid.SetColumn(label, item.Index);
                                     grid.Children.Add(label);
                                     break;

                                 case Button button:
                                     button.Click += DeleteSong;
                                     Grid.SetColumn(button, item.Index);
                                     grid.Children.Add(button);
                                     break;

                             }
                         });
            }

            SongListCollection.Add(grid);
        }
        public class SongMeta
        {
            public string Name { get; init; }
            public string Author { get; init; }
            public string Time { get; init; }
            public string Size { get; init; }
        }
        public class Song : SongMeta
        {
            public string Guid { get; init; }
        }
    }
}
