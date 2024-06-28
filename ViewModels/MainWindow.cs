using com.lZiMUl.BiliBili_Anchor_Assistant.ViewModels.Windows;
using System.Diagnostics;
using System.Windows;

namespace com.lZiMUl.BiliBili_Anchor_Assistant.ViewModels
{
    public partial class MainWindow
    {
        private readonly Home _home = new();
        public MainWindow()
        {
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            Home.AddWindow(this);
            CodeTips.Text = Config.LanguageResource.CodeTips;
            Activate.Content = Config.LanguageResource.Activate;
            BuyActivateCode.Content = Config.LanguageResource.BuyActivateCode;
            Exit.Content = Config.LanguageResource.Exit;

            if (Verification(Config.AppConfigurationManagerService.LoadConfig().ActivateCode))
            {
                Jump2Home();
            }
        }

        private void ActivateEvent(object sender, RoutedEventArgs e)
        {
            if (Verification(Code.Text))
            {
                Config.AppConfigurationManagerService.SaveConfig(new AppConfig
                {
                    ActivateCode = Code.Text
                });
                Jump2Home();
            }
            else
            {
                MessageBox.Show("激活码不存在");
            }
        }

        private void BuyActivateCodeEvent(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = Config.BuyActivateCodeUrl, UseShellExecute = true });
        }

        private void ExitEvent(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Jump2Home()
        {
            if (!_home.IsVisible)
            {
                _home.Show();
            }
            _home.Focus();
            Close();
        }

        private bool Verification(string? code)
        {
            if (code == "0123")
            {
                return true;
            }
            return false;
        }
    }
}
