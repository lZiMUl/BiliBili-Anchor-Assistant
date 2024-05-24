using System.Windows;
using System.Windows.Media.Imaging;

namespace BiliBili_Anchor_Assistant.Views
{
    public partial class MainWindow
    {
        // private static string url = "https://cn.bing.com";
        public MainWindow()
        {
            InitializeComponent();
            Icon = Config.Icon;
        }
        private void ConnectButton(object sender, RoutedEventArgs e)
        {
            // string data = await Http.Get(url);
            new SongManager().ShowDialog();
            // Console.Out.WriteLine(data);
        }
    }
}
