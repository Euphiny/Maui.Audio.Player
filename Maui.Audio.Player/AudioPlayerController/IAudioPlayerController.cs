namespace Maui.Audio.Player.AudioPlayerController;

public interface IAudioPlayerController
{
    public PlayerInfo PlayerInfo { get; }
    
    public void Start(string url, MediaInfo mediaInfo);

    public void Play();
    public void Pause();
}