using BiliBili_Anchor_Assistant.Tools;
using System.Windows;

namespace BiliBili_Anchor_Assistant.Views
{
    public partial class MainWindow
    {
        private static readonly List<Window> Windows = new();
        private readonly PlaySound _playSound = new();
        private SongManager? _songManager;
        public MainWindow()
        {
            Icon = Config.Icon;
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            Windows.Add(this);
        }

        private void ConnectButton(object sender, RoutedEventArgs e)
        {
            string roomId = RoomId.Text;
            Config.Uri.Query = $"?/roomId={roomId}";
            MessageBox.Show("You're wondering what's going on, aren't you? I haven't finished developing this software yet. Just wait a little longer. :) (By lZiMUl)");
            _playSound.Completed(SoundCompleted).Play(SoundType.WindowsHardwareRemove);
            // string data = await Http.Get(Config.Uri.ToString());

            // Console.Out.WriteLine(data);
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
        private void SoundCompleted(object sender, EventArgs e)
        {
            _playSound.Play(SoundType.WindowsHardwareInsert);
        }
    }
}
