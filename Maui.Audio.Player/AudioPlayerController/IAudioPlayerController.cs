namespace Maui.Audio.Player.AudioPlayerController;

public interface IAudioPlayerController
{
    public void Start(string url, MediaInfo mediaInfo);

    public void Play();
    public void Pause();
}