namespace Maui.Audio.Player.AudioPlayer;

public partial class AudioPlayer : IAudioPlayer
{
    public double Progress { get; }
    public double Duration { get; }
    public bool IsPlaying { get; }

    public AudioPlayer(string url, double duration) {}
    
    public void Play()
    {
        throw new NotImplementedException();
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }
    
    public void Dispose(bool disposing) {}
}