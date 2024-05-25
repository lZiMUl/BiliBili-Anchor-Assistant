using System.Windows.Media.Imaging;

namespace BiliBili_Anchor_Assistant
{
    public struct Config
    {
        public static readonly BitmapFrame Icon = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/Images/Icon.ico", UriKind.RelativeOrAbsolute));
        public static readonly UriBuilder Uri = new("https://cn.bing.com");
    }
}
