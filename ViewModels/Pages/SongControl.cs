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


        // # Bugs 播放器控制功能
        private void TouchDownEvent(object sender, RoutedEventArgs e)
        {
            var image = sender as Image;
            switch (image?.Name)
            {
                case "Previous":
                    PreviousSong();
                    break;

                case "Play":
                    image.Name = "Pause";
                    break;

                case "Pause":
                    image.Name = "Play";
                    break;

                case "Next":
                    NextSong();
                    break;

                default:

                    throw new SyntaxErrorException(ToString());
            }
        }

        private static void PreviousSong()
        {

        }


    private static void NextSong()
        {

        }
    }
}
