namespace Maui.Audio.Player.AudioPlayerController;

public interface IAudioPlayerController
{
    public event EventHandler? PlaybackEnded;
    
    public PlayerInfo PlayerInfo { get; }
    
    public void Start(Uri url, MediaInfo mediaInfo);

    public void Play();
    public void Pause();
    
    public void Seek(double positionInSeconds);
}