using System.ComponentModel;
using System.Media;

namespace BiliBili_Anchor_Assistant.Tools
{
    public class PlaySound : Event<SoundPlayer>
    {
        public void Play(SoundType soundType)
        {
            switch (soundType)
            {
                case SoundType.WindowsHardwareInsert:
                    Emit("playCompleted", new SoundPlayer(@"C:\Windows\Media\Windows Hardware Insert.wav"));
                    break;

                case SoundType.WindowsHardwareRemove:
                    Emit("playCompleted", new SoundPlayer(@"C:\Windows\Media\Windows Hardware Remove.wav"));
                    break;
            }
        }
        public PlaySound Completed(AsyncCompletedEventHandler asyncCompletedEventHandler)
        {
            AddEventListener("playCompleted", player =>
            {
                player.LoadCompleted += asyncCompletedEventHandler;
                player.Play();
            });
            return this;
        }
    }
}
