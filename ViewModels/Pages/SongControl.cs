using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace com.lZiMUl.BiliBili_Anchor_Assistant.ViewModels.Pages
{
    public partial class SongControl
    {
        public SongControl()
        {
            InitializeComponent();
            Previous.Source = Config.ImageResource.Previous;
            Play.Source = Config.ImageResource.Play;
            Next.Source = Config.ImageResource.Next;
        }

        private void TouchDownEvent(object sender, RoutedEventArgs e)
        {

            switch ((sender as Image)?.Name)
            {
                case "Previous":
                    break;

                case "Play":
                    break;

                case "Pause":
                    break;

                case "Next":
                    break;

                default:

                    throw new SyntaxErrorException(ToString());
            }
        }
    }
}
