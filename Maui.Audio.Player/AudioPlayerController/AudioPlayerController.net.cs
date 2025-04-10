namespace Maui.Audio.Player.AudioPlayerController;

public partial class AudioPlayerController : IAudioPlayerController
{
    public PlayerInfo PlayerInfo { get; } = null!;
    public void Start(string url, MediaInfo mediaInfo) { }

    public void Play() { }

    public void Pause() { }
}