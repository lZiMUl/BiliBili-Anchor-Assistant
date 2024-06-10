using BiliBili_Anchor_Assistant.Helper;
using BiliBili_Anchor_Assistant.Views;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Media.Imaging;

namespace BiliBili_Anchor_Assistant
{
    public class AppConfig
    {
        public bool? RequiredReboot { get; init; }
        public string? Language { get; init; }
        public int? RoomId { get; init; }
    }

    public struct Config
    {
        public static readonly string AppFilePath = Assembly.GetExecutingAssembly().Location;
        public static readonly string AppFileName = Path.GetFileName(AppFilePath).Replace("dll", "exe");
        public static readonly BitmapFrame Icon = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/Images/Icon.ico", UriKind.RelativeOrAbsolute));
        public static readonly ConfigurationManagerHelper<AppConfig> AppConfigurationManagerHelper = new("DefaultConfig.json", new AppConfig
        {
            RequiredReboot = false,
            Language = "EnUs",
            RoomId = 9329583
        }, true);
        public static readonly ConfigurationManagerHelper<List<SongManager.Song>> SongListConfigurationManagerHelper = new("SongListConfig.json", new List<SongManager.Song>(), false);
        public static readonly string AppHomeUrl = "https://lzimul.top";
        public static readonly UriBuilder GetRoomId = new("https://api.live.bilibili.com/room/v1/Room/room_init");
        public static readonly UriBuilder GetChatServer = new("https://api.live.bilibili.com/room/v1/Danmu/getConf");
        private static readonly ResourceManager EnglishResourceManager = new("BiliBili_Anchor_Assistant.Resources.Languages.en_US", Assembly.GetExecutingAssembly());
        private static readonly ResourceManager ChineseResourceManager = new("BiliBili_Anchor_Assistant.Resources.Languages.zh_CN", Assembly.GetExecutingAssembly());

        private static string GetLocalizedString(string resourceName)
        {

            switch (AppConfigurationManagerHelper.LoadConfig().Language)
            {
                case "EnUs":
                    return EnglishResourceManager.GetString(resourceName);

                case "ZhCn":
                    return ChineseResourceManager.GetString(resourceName);

                default:
                    return EnglishResourceManager.GetString(resourceName);
            }
        }

        public static class LanguageResource
        {
            public static readonly string Menus = GetLocalizedString("menus");
            public static readonly string Settings = GetLocalizedString("settings");
            public static readonly string Languages = GetLocalizedString("languages");
            public static readonly string About = GetLocalizedString("about");
            public static readonly string Warning = GetLocalizedString("warning");
            public static readonly string WarningContent = GetLocalizedString("warning_content");
            public static readonly string EnUs = GetLocalizedString("en_us");
            public static readonly string ZhCn = GetLocalizedString("zh_cn");
            public static readonly string RoomId = GetLocalizedString("room_id");
            public static readonly string Disconnected = GetLocalizedString("disconnected");
            public static readonly string Connect = GetLocalizedString("connect");
            public static readonly string Waiting = GetLocalizedString("waiting");
            public static readonly string Connected = GetLocalizedString("connected");
            public static readonly string Disconnect = GetLocalizedString("disconnect");
            public static readonly string SongManager = GetLocalizedString("song_manager");
            public static readonly string ImmediateRestart = GetLocalizedString("immediate_restart");
            public static readonly string LaterRestart = GetLocalizedString("later_restart");
            public static readonly string AddSong = GetLocalizedString("add_song");
            public static readonly string Delete = GetLocalizedString("delete");
        }
    }
}
