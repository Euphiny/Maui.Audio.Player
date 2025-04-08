namespace Maui.Audio.Player.AudioPlayer;

public partial class AudioPlayer : IAudioPlayer
{
    public double Progress { get; }
    public double Duration { get; }
    
    public AudioPlayer(string url, double duration) {}
    
    public void Play() {}

    public void Pause() { }
    
    public void Dispose(bool disposing) {}
}