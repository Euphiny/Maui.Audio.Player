using Maui.Audio.Player.AudioPlayer;
using Maui.Audio.Player.MediaInfoManager;

namespace Maui.Audio.Player.AudioPlayerController;

public partial class AudioPlayerController : IAudioPlayerController
{
    private readonly IMediaInfoManager _mediaInfoManager;
    
    private IAudioPlayer? _player;
    private MediaInfo? _mediaInfo;
    
    public event EventHandler? PlaybackEnded;
    
    public void Play()
    {
        if (_player == null || _mediaInfo == null)
            throw new NullReferenceException();
        
        _player.Play();
        _mediaInfoManager.SetMediaInfo(_mediaInfo);
    }

    public void Pause()
    {
        if (_player == null)
            throw new NullReferenceException();
        
        _player.Pause();
    }

    public void Seek(double positionInSeconds)
    {
        _player?.Seek(positionInSeconds);
    }
}