using com.lZiMUl.BiliBili_Anchor_Assistant.Services;
using com.lZiMUl.BiliBili_Anchor_Assistant.Tools;
using com.lZiMUl.BiliBili_Anchor_Assistant.ViewModels.Windows;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Media.Imaging;

namespace com.lZiMUl.BiliBili_Anchor_Assistant
{
    public class AppConfig
    {
        public bool RequiredReboot { get; init; }
        public string Language { get; init; }
        public int? RoomId { get; init; }
    }

    public struct Config
    {
        public static readonly string AppFileName = Assembly.GetExecutingAssembly().GetName().Name ?? "BiliBili Anchor Assistant";
        public static readonly ConfigurationManagerService<AppConfig> AppConfigurationManagerService = new("DefaultConfig.json", new AppConfig
        {
            RequiredReboot = false,
            Language = "EnUs",
            RoomId = null
        }, true);
        public static readonly ConfigurationManagerService<List<SongManager.Song>> SongListConfigurationManagerService = new("SongListConfig.json", new List<SongManager.Song>(), false);
        public static readonly string AppHomeUrl = "https://lzimul.top";
        public static readonly UriBuilder GetRoomId = new("https://api.live.bilibili.com/room/v1/Room/room_init");
        public static readonly UriBuilder GetChatServer = new("https://api.live.bilibili.com/room/v1/Danmu/getConf");
        public static readonly ResourceManager EnglishResourceManager = new("com.lZiMUl.BiliBili_Anchor_Assistant.Resources.Languages.en_US", Assembly.GetExecutingAssembly());
        public static readonly ResourceManager ChineseResourceManager = new("com.lZiMUl.BiliBili_Anchor_Assistant.Resources.Languages.zh_CN", Assembly.GetExecutingAssembly());

        public static class ImageResource
        {
            public static readonly BitmapImage Previous = LocalResource.GetUiImage("previous");
            public static readonly BitmapImage Play = LocalResource.GetUiImage("play");
            public static readonly BitmapImage Pause = LocalResource.GetUiImage("pause");
            public static readonly BitmapImage Next = LocalResource.GetUiImage("next");
        }

        public static class LanguageResource
        {
            public static readonly string Menus = LocalResource.GetLocalizedString("menus");
            public static readonly string Settings = LocalResource.GetLocalizedString("settings");
            public static readonly string Languages = LocalResource.GetLocalizedString("languages");
            public static readonly string About = LocalResource.GetLocalizedString("about");
            public static readonly string Warning = LocalResource.GetLocalizedString("warning");
            public static readonly string WarningContent = LocalResource.GetLocalizedString("warning_content");
            public static readonly string EnUs = LocalResource.GetLocalizedString("en_us");
            public static readonly string ZhCn = LocalResource.GetLocalizedString("zh_cn");
            public static readonly string RoomId = LocalResource.GetLocalizedString("room_id");
            public static readonly string Disconnected = LocalResource.GetLocalizedString("disconnected");
            public static readonly string Connect = LocalResource.GetLocalizedString("connect");
            public static readonly string Waiting = LocalResource.GetLocalizedString("waiting");
            public static readonly string Connected = LocalResource.GetLocalizedString("connected");
            public static readonly string Disconnect = LocalResource.GetLocalizedString("disconnect");
            public static readonly string SongManager = LocalResource.GetLocalizedString("song_manager");
            public static readonly string ImmediateRestart = LocalResource.GetLocalizedString("immediate_restart");
            public static readonly string LaterRestart = LocalResource.GetLocalizedString("later_restart");
            public static readonly string AddSong = LocalResource.GetLocalizedString("add_song");
            public static readonly string Title = LocalResource.GetLocalizedString("title");
            public static readonly string Author = LocalResource.GetLocalizedString("author");
            public static readonly string Time = LocalResource.GetLocalizedString("time");
            public static readonly string Size = LocalResource.GetLocalizedString("size");
            public static readonly string Action = LocalResource.GetLocalizedString("action");
            public static readonly string Delete = LocalResource.GetLocalizedString("delete");
        }
    }
}
