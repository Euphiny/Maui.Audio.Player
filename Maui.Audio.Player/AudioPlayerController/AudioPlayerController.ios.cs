using Maui.Audio.Player.AudioPlayer;
using Maui.Audio.Player.MediaInfoManager;

namespace Maui.Audio.Player.AudioPlayerController;

public partial class AudioPlayerController : IAudioPlayerController
{
    private readonly IMediaInfoManager _mediaInfoManager;
    
    private IAudioPlayer? _player;
    private MediaInfo? _mediaInfo;

    public PlayerInfo PlayerInfo => new(_player?.Duration ?? 0, _player?.Progress ?? 0, _player?.IsPlaying ?? false);
    
    public AudioPlayerController(IMediaInfoManager mediaInfoManager)
    {
        _mediaInfoManager = mediaInfoManager;
    }

    public void Start(string url, MediaInfo mediaInfo)
    {
        Stop();
        
        _mediaInfo = mediaInfo;
        
        _player = new AudioPlayer.AudioPlayer(url, mediaInfo.Duration);
        _player.PlaybackStopped += PlaybackEnded;
        
        Play();
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

    private void Stop()
    {
        if (_player == null)
            return;
        
        _player.PlaybackStopped -= PlaybackEnded;
        _player.Dispose();
    }
    
    private void PlaybackEnded(object? sender, EventArgs eventArgs)
    {
        Stop();
    }
}