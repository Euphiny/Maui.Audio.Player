using Maui.Audio.Player.AudioPlayer;
using Maui.Audio.Player.MediaInfoManager;

namespace Maui.Audio.Player.AudioPlayerController;

public partial class AudioPlayerController : IAudioPlayerController
{
    private readonly IMediaInfoManager _mediaInfoManager;
    private IAudioPlayer? _player;

    public AudioPlayerController(IMediaInfoManager mediaInfoManager)
    {
        _mediaInfoManager = mediaInfoManager;
    }
    
    public void Start(string url, MediaInfo mediaInfo)
    {
        _player = new AudioPlayer.AudioPlayer(url, mediaInfo.Duration);
        _mediaInfoManager.SetMediaInfo(mediaInfo);
        
        _player.Play();
    }

    public void Play()
    {
        throw new NotImplementedException();
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }
}