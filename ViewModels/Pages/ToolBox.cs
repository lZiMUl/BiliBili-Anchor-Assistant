using com.lZiMUl.BiliBili_Anchor_Assistant.ViewModels.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace com.lZiMUl.BiliBili_Anchor_Assistant.ViewModels.Pages
{
    public partial class ToolBox : Page
    {
        public ToolBox()
        {
            InitializeComponent();
            Menus.Header = Config.LanguageResource.Menus;
            SettingsMenu.Header = Config.LanguageResource.Settings;
            LanguagesMenu.Header = Config.LanguageResource.Languages;
            AboutMenu.Header = Config.LanguageResource.About;

            EnUs.Header = Config.LanguageResource.EnUs;
            ZhCn.Header = Config.LanguageResource.ZhCn;
        }

        private void MenuEvent(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(":)");
        }

        private void LanguagesChooseEvent(object sender, RoutedEventArgs e)
        {
            var clickedItem = sender as MenuItem;
            var dialog = new Dialog(Config.LanguageResource.Warning, Config.LanguageResource.WarningContent, clickedItem);
            var appConfig = Config.AppConfigurationManagerService.LoadConfig();

            if (clickedItem.Name != appConfig.Language
                && !appConfig.RequiredReboot
                && !dialog.IsVisible)
            {
                dialog.Show();
            }
        }

        private void AboutEvent(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = Config.AppHomeUrl, UseShellExecute = true });
        }
    }
}
