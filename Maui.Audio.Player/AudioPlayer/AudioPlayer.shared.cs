namespace Maui.Audio.Player.AudioPlayer;

public partial class AudioPlayer : IAudioPlayer
{
#pragma warning disable CS0067
    public event EventHandler? PlaybackEnded;
#pragma warning restore CS0067
    
    public void Dispose()
    {
        Dispose(true);
        
        GC.SuppressFinalize(this);
    }
}