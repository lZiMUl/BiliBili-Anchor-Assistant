using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace BiliBili_Anchor_Assistant.Views
{
    public partial class Dialog
    {
        private readonly MenuItem _clickedItem;

        public Dialog(string title, string message, MenuItem clickedItem)
        {
            Icon = Config.Icon;
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            MainWindow.AddWindow(this);
            Title = title;
            Message.Text = message;
            _clickedItem = clickedItem;
            ImmediateRestartButton.Content = Config.LanguageResource.ImmediateRestart;
            LaterRestartButton.Content = Config.LanguageResource.LaterRestart;
        }

        private void ImmediateRestartButtonEvent(object sender, RoutedEventArgs e)
        {
            Config.AppConfigurationManagerHelper.SaveConfig(new AppConfig
            {
                RequiredReboot = true,
                Language = _clickedItem.Name
            });
            var startInfo = new ProcessStartInfo
            {
                FileName = Config.AppFileName,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (var exeProcess = Process.Start(startInfo))
            {
                exeProcess?.WaitForInputIdle();
            }

            Environment.Exit(0);
        }
        private void LaterRestartButtonEvent(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
