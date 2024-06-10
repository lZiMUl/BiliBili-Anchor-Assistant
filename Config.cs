using System.Windows.Media.Imaging;

namespace BiliBili_Anchor_Assistant
{
    public struct Config
    {
        public static readonly BitmapFrame Icon = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/Images/Icon.ico", UriKind.RelativeOrAbsolute));
        public static readonly UriBuilder GetRoomId = new("https://api.live.bilibili.com/room/v1/Room/room_init");
        public static readonly UriBuilder GetChatServer = new("https://api.live.bilibili.com/room/v1/Danmu/getConf");
        // public static readonly UriBuilder Uri = new("");
        // public static readonly UriBuilder Uri = new("");
        // public static readonly UriBuilder Uri = new("");
    }
}
