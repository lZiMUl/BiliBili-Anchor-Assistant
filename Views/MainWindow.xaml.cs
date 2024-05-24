using System.Windows;
using System.Windows.Controls;

using BiliBili_Anchor_Assistant.Tools;
using BiliBili_Anchor_Assistant.Views;

namespace BiliBili_Anchor_Assistant;

public partial class MainWindow : Window
{
    // private static string url = "https://cn.bing.com";
    public MainWindow()
    {
        InitializeComponent();
    }
    private void ConnectButton(object sender, RoutedEventArgs e)
    {
        // string data = await Http.Get(url);
        SongManager songManager = new SongManager();
        Window window = new Window();
        window.Content = new Frame()
        {
            Content = songManager
        };
        window.ShowDialog();
        // Console.Out.WriteLine(data);
    }
}
