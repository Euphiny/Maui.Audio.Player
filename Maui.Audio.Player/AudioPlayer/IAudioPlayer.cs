namespace Maui.Audio.Player.AudioPlayer;

public interface IAudioPlayer : IDisposable
{
    public event EventHandler PlaybackEnded;
    
    [Obsolete("Use CurrentProgress instead.")]
    public double Progress { get; }
    [Obsolete("Use TotalDuration instead.")]
    public double Duration { get; }
    
    public TimeSpan CurrentProgress { get; }
    public TimeSpan TotalDuration { get; }
    
    public bool IsPlaying { get; }
    
    public void Play();

    public void Pause();
    
    public void Seek(double position);
}