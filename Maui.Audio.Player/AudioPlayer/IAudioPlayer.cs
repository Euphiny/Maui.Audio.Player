namespace Maui.Audio.Player.AudioPlayer;

public interface IAudioPlayer : IDisposable
{
    public double Progress { get; }
    public double Duration { get; }
    public bool IsPlaying { get; }
    
    public void Play();

    public void Pause();
}