using System.Resources;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace BiliBili_Anchor_Assistant
{
    public struct Config
    {
        public static readonly BitmapFrame Icon = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/Images/Icon.ico", UriKind.RelativeOrAbsolute));
        public static readonly UriBuilder GetRoomId = new("https://api.live.bilibili.com/room/v1/Room/room_init");
        public static readonly UriBuilder GetChatServer = new("https://api.live.bilibili.com/room/v1/Danmu/getConf");
        private static readonly ResourceManager ResourceManager = new("BiliBili_Anchor_Assistant", Assembly.GetExecutingAssembly());



        public static string GetLocalizedString(string resourceName)
        {

            string resourceValue = ResourceManager.GetString(resourceName);

            if (resourceValue == null)
            {
                // 如果资源未找到，则返回资源的键名（或你可以选择返回默认值）
                return $"#{resourceName}#";
            }

            // 返回找到的资源值
            return resourceValue;
        }
    }
}
