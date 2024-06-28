using System.Windows.Media.Imaging;

namespace com.lZiMUl.BiliBili_Anchor_Assistant.Tools
{
    public static class LocalResource
    {
        public static BitmapImage GetUiImage(string fileName) => new(new Uri($"pack://application:,,,/;component/Resources/Images/UI/{fileName}.png"));

        public static string GetLocalizedString(string resourceName)
        {

            switch (Config.AppConfigurationManagerService.LoadConfig().Language)
            {
                case "EnUs":
                    return Config.EnglishResourceManager.GetString(resourceName);

                case "ZhCn":
                    return Config.ChineseResourceManager.GetString(resourceName);

                default:
                    return Config.EnglishResourceManager.GetString(resourceName);
            }
        }
    }
}
