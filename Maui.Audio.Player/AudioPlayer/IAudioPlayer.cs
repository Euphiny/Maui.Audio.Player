namespace Maui.Audio.Player.AudioPlayer;

public interface IAudioPlayer : IDisposable
{
    public event EventHandler PlaybackStopped;
    
    public double Progress { get; }
    public double Duration { get; }
    public bool IsPlaying { get; }
    
    public void Play();

    public void Pause();
    
    public void Seek(double position);
}