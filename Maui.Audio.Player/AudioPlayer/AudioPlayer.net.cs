namespace Maui.Audio.Player.AudioPlayer;

public partial class AudioPlayer : IAudioPlayer
{
    public double Progress { get; }
    public double Duration { get; }
    public bool IsPlaying { get; }

    public AudioPlayer(string url, double duration) {}
    
    public void Play() {}
    public void Pause() { }
    public void Seek(double position) { }

    public void Dispose(bool disposing) {}
}