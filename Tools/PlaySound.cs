using com.lZiMUl.BiliBili_Anchor_Assistant.Enum;
using System.Media;

namespace com.lZiMUl.BiliBili_Anchor_Assistant.Tools
{
    public class PlaySound : Event<SoundPlayer>
    {
        public void Play(SoundTypeEnum soundTypeEnum)
        {
            AddEventListener("playCompleted", player =>
            {
                player.Play();
            });
            switch (soundTypeEnum)
            {
                case SoundTypeEnum.WindowsHardwareInsert:
                    Emit("playCompleted", new SoundPlayer(@"C:\Windows\Media\Windows Hardware Insert.wav"));
                    break;

                case SoundTypeEnum.WindowsHardwareRemove:
                    Emit("playCompleted", new SoundPlayer(@"C:\Windows\Media\Windows Hardware Remove.wav"));
                    break;
                case SoundTypeEnum.WindowsBackground:
                    Emit("playCompleted", new SoundPlayer(@"C:\Windows\Media\Windows Background.wav"));
                    break;
            }
        }
    }
}
