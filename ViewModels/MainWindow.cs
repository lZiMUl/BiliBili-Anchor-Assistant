using com.lZiMUl.BiliBili_Anchor_Assistant.Enum;
using com.lZiMUl.BiliBili_Anchor_Assistant.Tools;
using com.lZiMUl.BiliBili_Anchor_Assistant.ViewModels.Windows;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace com.lZiMUl.BiliBili_Anchor_Assistant.ViewModels
{
    public class Result
    {
        public int code { get; init; } = 0;
        public string message { get; init; } = string.Empty;
        public dynamic data { get; init; }
    }

    public partial class MainWindow
    {
        private static readonly List<Window> Windows = new();
        private static readonly BiliAPI BiliApi = new();
        private readonly PlaySound _playSound = new();
        private SongManager? _songManager;

        public MainWindow()
        {
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();

            Menus.Header = Config.LanguageResource.Menus;
            SettingsMenu.Header = Config.LanguageResource.Settings;
            LanguagesMenu.Header = Config.LanguageResource.Languages;
            AboutMenu.Header = Config.LanguageResource.About;

            EnUs.Header = Config.LanguageResource.EnUs;
            ZhCn.Header = Config.LanguageResource.ZhCn;

            RoomIdTextBlock.Text = Config.LanguageResource.RoomId;
            ConnectButton.Content = Config.LanguageResource.Disconnected;
            SongManagerButton.Content = Config.LanguageResource.SongManager;

            Windows.Add(this);
            var config = Config.AppConfigurationManagerService.LoadConfig();

            Config.AppConfigurationManagerService.SaveConfig(new AppConfig
            {
                RequiredReboot = false
            });

            RoomIdTextBox.Text = config.RoomId.ToString() ?? "";
            BiliApi.AddListener(EventTypeEnum.Join, data =>
            {

            });
            BiliApi.AddListener(EventTypeEnum.Message, data =>
            {
                _songManager?.AddSong(new SongManager.SongMeta
                {
                    Name = "测试(Test)",
                    Author = "lZiMUl",
                    Time = DateTime.Now.ToString("h:mm:ss"),
                    Size = "114514TB"
                });
            });
            BiliApi.AddListener(EventTypeEnum.Gift, data =>
            {

            });
        }

        private void MenuEvent(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(":)");
        }

        private void LanguagesChooseEvent(object sender, RoutedEventArgs e)
        {
            var clickedItem = sender as MenuItem;
            var dialog = new Dialog(Config.LanguageResource.Warning, Config.LanguageResource.WarningContent, clickedItem);
            var appConfig = Config.AppConfigurationManagerService.LoadConfig();

            if (clickedItem.Name != appConfig.Language
                && !appConfig.RequiredReboot
                && !dialog.IsVisible)
            {
                dialog.Show();
            }
        }

        private void AboutEvent(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = Config.AppHomeUrl, UseShellExecute = true });
        }

        private async void ConnectButtonEvent(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.Content.ToString() != Config.LanguageResource.Disconnect)
            {
                WaitingEvent(button);
                try
                {
                    string roomId = RoomIdTextBox.Text;
                    var reg = new Regex("^[0-9]{1,15}$");
                    if (reg.Match(IntPtr.Parse(roomId).ToString()).Success)
                    {
                        Config.Api.GetRoomId.Query = $"?id={roomId}";
                        var data = await Http.Get<Result>(Config.Api.GetRoomId.ToString());
                        if (data.code == 0)
                        {
                            ConnectEvent(button);
                            RoomIdTextBox.IsEnabled = false;
                            Config.AppConfigurationManagerService.SaveConfig(new AppConfig
                                {
                                    RoomId = int.Parse(roomId)
                                }
                            );
                            MessageBox.Show(data.data.ToString());
                        }
                        else
                        {
                            RoomIdTextBox.IsEnabled = true;
                            MessageBox.Show(data.message);
                            DisconnectEvent(button);
                        }
                    }
                    else throw new Exception();
                }
                catch (Exception exception)
                {
                    RoomIdTextBox.Text = string.Empty;
                    MessageBox.Show("非法内容");
                    DisconnectEvent(button);
                }
            }
            else DisconnectEvent(button);
            RoomIdTextBox.IsEnabled = true;

        }
        private void SongManagerButtonEvent(object sender, RoutedEventArgs e)
        {
            _songManager = new SongManager();
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

        private void ConnectEvent(Button button)
        {
            button.Content = Config.LanguageResource.Connected;
            button.Background = Brushes.MediumSpringGreen;
            _playSound.Play(SoundTypeEnum.WindowsHardwareInsert);
            BiliApi.Start();
        }

        private void WaitingEvent(Button button)
        {
            button.Content = Config.LanguageResource.Waiting;
            button.Background = Brushes.Orange;
            _playSound.Play(SoundTypeEnum.WindowsBackground);
        }

        private void DisconnectEvent(Button button)
        {
            button.Content = Config.LanguageResource.Disconnected;
            button.Background = Brushes.OrangeRed;
            _playSound.Play(SoundTypeEnum.WindowsHardwareRemove);
            BiliApi.Stop();
        }

        private void ConnectButtonMouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;

            if (button?.Content as string == Config.LanguageResource.Connected)
            {
                button.Content = Config.LanguageResource.Disconnect;
                button.Background = Brushes.OrangeRed;
            }
            else if (button?.Content as string == Config.LanguageResource.Disconnected)
            {
                button.Content = Config.LanguageResource.Connect;
                button.Background = Brushes.MediumSpringGreen;
            }
        }

        private void ConnectButtonMouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;

            if (button?.Content as string == Config.LanguageResource.Connect)
            {
                button.Content = Config.LanguageResource.Disconnected;
                button.Background = Brushes.OrangeRed;
            }
            else if (button?.Content as string == Config.LanguageResource.Disconnect)
            {
                button.Content = Config.LanguageResource.Connected;
                button.Background = Brushes.MediumSpringGreen;
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
