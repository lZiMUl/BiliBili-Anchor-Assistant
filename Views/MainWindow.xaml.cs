using BiliBili_Anchor_Assistant.Enum;
using BiliBili_Anchor_Assistant.Helper;
using BiliBili_Anchor_Assistant.Tools;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BiliBili_Anchor_Assistant.Views
{
    public class Default
    {
        public string RoomId { get; init; } = string.Empty;
    }


    public class Result
    {
        public int code { get; init; } = 0;
        public string message { get; init; } = string.Empty;
        public dynamic data { get; init; }
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

            Menus.Header = Config.GetLocalizedString("menu");
            Windows.Add(this);
            var config = JsonHelper.LoadConfig();
            if (config.Count > 0)
            {
                RoomIdTextBox.Text = config[0].RoomId;
            }
            BiliApi.AddListener(EventTypeEnum.Join, data =>
            {

            });
            BiliApi.AddListener(EventTypeEnum.Message, data =>
            {
                _songManager?.AddSong(new SongManager.SongMeta
                {
                    Name = "a",
                    Author = "b",
                    Time = "c",
                    Size = "d"
                });
            });
            BiliApi.AddListener(EventTypeEnum.Gift, data =>
            {

            });
        }

        private void Menu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("没做");
        }

        private void LanguagesMenu(object sender, RoutedEventArgs e)
        {
            var clickedItem = sender as MenuItem;

            string header = clickedItem?.Header as string;
            MessageBox.Show($"Clicked: {header}");
            switch (header)
            {

            }
        }

        private async void ConnectButtonEvent(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.Content.ToString() != "Disconnect")
            {
                Waiting(button);
                try
                {
                    string roomId = RoomIdTextBox.Text;
                    var reg = new Regex("^[0-9]{1,15}$");
                    if (reg.Match(IntPtr.Parse(roomId).ToString()).Success)
                    {
                        Config.GetRoomId.Query = $"?id={roomId}";
                        var data = await Http.Get<Result>(Config.GetRoomId.ToString());
                        Console.Out.WriteLine(data.data);
                        if (data.code == 0)
                        {
                            RoomIdTextBox.IsEnabled = false;
                            // MessageBox.Show(data.data.room_id);
                            Connect(button);
                            JsonHelper.SaveConfig(new List<Default>
                            {
                                new()
                                {
                                    RoomId = RoomIdTextBox.Text
                                }
                            });
                        }
                        else
                        {
                            RoomIdTextBox.IsEnabled = true;
                            MessageBox.Show(data.message);
                            Disconnect(button);
                        }
                    }
                    else throw new Exception();
                }
                catch (Exception exception)
                {
                    RoomIdTextBox.Text = string.Empty;
                    MessageBox.Show("非法内容");
                    Disconnect(button);
                }
            }
            else Disconnect(button);
            RoomIdTextBox.IsEnabled = true;

        }
        private void SongManagerButtonEvent(object sender, RoutedEventArgs e)
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

        private void Connect(Button button)
        {
            button.Content = "Connected";
            button.Background = Brushes.MediumSpringGreen;
            _playSound.Play(SoundTypeEnum.WindowsHardwareInsert);
            BiliApi.Start();
        }

        private void Waiting(Button button)
        {
            button.Content = "Waiting";
            button.Background = Brushes.Orange;
            _playSound.Play(SoundTypeEnum.WindowsBackground);
        }

        private void Disconnect(Button button)
        {
            button.Content = "Disconnected";
            button.Background = Brushes.OrangeRed;
            _playSound.Play(SoundTypeEnum.WindowsHardwareRemove);
            BiliApi.Stop();
        }

        private void ConnectButtonMouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            switch (button?.Content)
            {
                case "Connected":
                    button.Content = "Disconnect";
                    button.Background = Brushes.OrangeRed;
                    break;

                case "Disconnected":
                    button.Content = "Connect";
                    button.Background = Brushes.MediumSpringGreen;
                    break;
            }
        }

        private void ConnectButtonMouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            switch (button?.Content)
            {
                case "Connect":
                    button.Content = "Disconnected";
                    button.Background = Brushes.OrangeRed;
                    break;

                case "Disconnect":
                    button.Content = "Connected";
                    button.Background = Brushes.MediumSpringGreen;
                    break;
            }
        }

        private void SongManagerButtonMouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Background = Brushes.Chocolate;
        }

        private void SongManagerButtonMouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Background = Brushes.Tan;
        }
    }
}
