namespace Maui.Audio.Player.AudioPlayer;

public partial class AudioPlayer : IAudioPlayer
{
    public void Dispose()
    {
        Dispose(true);
        
        GC.SuppressFinalize(this);
    }
}