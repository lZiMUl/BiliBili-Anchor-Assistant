using BiliBili_Anchor_Assistant.Enum;
using BiliBili_Anchor_Assistant.Helper;
using BiliBili_Anchor_Assistant.Tools;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BiliBili_Anchor_Assistant.Views
{
    public class Default
    {
        public string RoomId { get; init; }
    }

    public class IA {
        public int code { get; }
        public string message { get; }
    }

    public partial class MainWindow
    {
        private static readonly JsonHelper<Default> JsonHelper = new("DefaultConfig.json");
        private static readonly List<Window> Windows = new();
        private static readonly BiliAPI BiliApi = new();
        private readonly PlaySound _playSound = new();
        private SongManager? _songManager;
        public MainWindow()
        {
            Icon = Config.Icon;
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            Windows.Add(this);
            var config = JsonHelper.LoadConfig();
            if (config.Count > 0)
            {
                RoomId.Text = config[0].RoomId;
            }
            BiliApi.AddListener(EventTypeEnum.Join, s =>
            {

            });
            BiliApi.AddListener(EventTypeEnum.Message, s =>
            {
                _songManager.AddSong(new SongManager.SongMeta()
                {
                    Name = "a",
                    Author = "b",
                    Time = "c",
                    Size = "d"
                });
            });
            BiliApi.AddListener(EventTypeEnum.Gift, s =>
            {

            });
        }

        private async void ConnectButton(object sender, RoutedEventArgs e)
        {
            var myself = sender as Button;
            JsonHelper.SaveConfig(new List<Default>
            {
                new()
                {
                    RoomId = RoomId.Text
                }
            });

            if (myself.Content.ToString() != "Disconnect")
            {
                myself.Content = "Disconnect";
                myself.Background = Brushes.OrangeRed;
                _playSound.Play(SoundTypeEnum.WindowsHardwareInsert);
                BiliApi.Start();
                string roomId = RoomId.Text;
                Config.GetRoomId.Query = $"?id={roomId}";
                var data = await Http.Get<IA>(Config.GetRoomId.ToString());
                if (data.code == 0)
                {
                    Console.Out.WriteLine(data);
                }
                else
                {
                    MessageBox.Show(data.message);
                }
            }
            else
            {
                myself.Content = "Connect";
                myself.Background = Brushes.MediumSpringGreen;
                _playSound.Play(SoundTypeEnum.WindowsHardwareRemove);
                BiliApi.Stop();
            }

        }
        private void SongManager(object sender, RoutedEventArgs e)
        {
            _songManager = Application.Current.Windows.OfType<SongManager>().FirstOrDefault() ?? new SongManager();
            if (!_songManager.IsVisible)
            {
                _songManager.Show();
            }
            _songManager.Focus();
        }

        public static void AddWindow(Window window)
        {
            Windows.Add(window);
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Windows.ForEach(window =>
            {
                if (window != this) window.Close();
            });
        }
    }
}
