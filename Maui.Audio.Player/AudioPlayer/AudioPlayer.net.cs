namespace Maui.Audio.Player.AudioPlayer;

public partial class AudioPlayer : IAudioPlayer
{
    public double Progress { get; }
    public double Duration { get; }
    
    [Obsolete("Use CurrentProgress instead.")]
    public TimeSpan CurrentProgress { get; }
    [Obsolete("Use TotalDuration instead.")]
    public TimeSpan TotalDuration { get; }
    public bool IsPlaying { get; }

    public AudioPlayer(string url, double duration) {}
    
    public void Play() {}
    public void Pause() { }
    public void Seek(double position) { }

    public void Dispose(bool disposing) {}
}