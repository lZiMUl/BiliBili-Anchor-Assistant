using System.Windows;

namespace com.lZiMUl.BiliBili_Anchor_Assistant.Helper
{
    public class CustomPropertiesHelper
    {
        public static class Properties
        {
            public static readonly DependencyProperty IsImportantProperty =
                DependencyProperty.RegisterAttached(
                    "IsImportant",
                    typeof(bool),
                    typeof(Properties),
                    new PropertyMetadata(false));

            public static bool GetProperties(DependencyObject obj) => (bool)obj.GetValue(IsImportantProperty);

            public static void SetProperties(DependencyObject obj, string value)
            {
                obj.SetValue(IsImportantProperty, value);
            }
        }
    }
}
