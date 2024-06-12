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
    }
}
