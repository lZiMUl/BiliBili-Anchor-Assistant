using BiliBili_Anchor_Assistant.Tools;
using System.Windows;

namespace BiliBili_Anchor_Assistant.Views
{
    public partial class MainWindow
    {
        private static readonly UriBuilder Uri = new("https://cn.bing.com");
        private SongManager? _songManager;
        public MainWindow()
        {
            InitializeComponent();
            Icon = Config.Icon;
        }
        private async void ConnectButton(object sender, RoutedEventArgs e)
        {
            string roomId = RoomID.Text;
            Uri.Query = "?/6";
            string data = await Http.Get(Uri.ToString());

            Console.Out.WriteLine(data);
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
    }
}
