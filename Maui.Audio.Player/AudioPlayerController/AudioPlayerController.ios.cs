using Maui.Audio.Player.AudioPlayer;
using Maui.Audio.Player.MediaInfoManager;

namespace Maui.Audio.Player.AudioPlayerController;

public partial class AudioPlayerController : IAudioPlayerController
{
    private readonly IMediaInfoManager _mediaInfoManager;
    
    private IAudioPlayer? _player;
    private MediaInfo? _mediaInfo;

    public PlayerInfo PlayerInfo { get; }
    
    public AudioPlayerController(IMediaInfoManager mediaInfoManager)
    {
        _mediaInfoManager = mediaInfoManager;
    }

    public void Start(string url, MediaInfo mediaInfo)
    {
        _player?.Dispose();
        
        _player = new AudioPlayer.AudioPlayer(url, mediaInfo.Duration);
        _mediaInfo = mediaInfo;
    }

    public void Play()
    {
        if (_mediaInfo == null || _player == null)
            throw new NullReferenceException();
        
        _mediaInfoManager.SetMediaInfo(_mediaInfo);
        _player.Play();
    }

    public void Pause()
    {
        _player?.Pause();
    }
}