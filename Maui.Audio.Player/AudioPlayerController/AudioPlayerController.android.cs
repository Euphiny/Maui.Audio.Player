namespace Maui.Audio.Player.AudioPlayerController;

public partial class AudioPlayerController : IAudioPlayerController
{
    public PlayerInfo PlayerInfo { get; }
    
    public void Start(string url, MediaInfo mediaInfo)
    {
        throw new NotImplementedException();
    }

    public void Play()
    {
        throw new NotImplementedException();
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }

    public void Seek(double positionInSeconds)
    {
        throw new NotImplementedException();
    }
}