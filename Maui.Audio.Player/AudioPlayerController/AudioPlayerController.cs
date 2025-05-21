using Maui.Audio.Player.AudioPlayer;
using Maui.Audio.Player.MediaInfoManager;

namespace Maui.Audio.Player.AudioPlayerController;

public class AudioPlayerController : IAudioPlayerController
{
    private readonly IMediaInfoManager _mediaInfoManager;
    
    private IAudioPlayer? _player;
    private MediaInfo? _mediaInfo;
    
    public PlayerInfo PlayerInfo => new(_player?.TotalDuration, _player?.CurrentProgress, _player?.IsPlaying ?? false);
    
    public event EventHandler? PlaybackEnded;
    
    public AudioPlayerController(IMediaInfoManager mediaInfoManager)
    {
        _mediaInfoManager = mediaInfoManager;

        _mediaInfoManager.Initialize();
        
        _mediaInfoManager.SetPauseCommand(Pause);
        _mediaInfoManager.SetPlayCommand(Play);
    }
    
    public void Start(string url, MediaInfo mediaInfo)
    {
        Stop();
        
        _mediaInfo = mediaInfo;
        
        _player = new AudioPlayer.AudioPlayer(url, mediaInfo.TotalDuration);
        _player.PlaybackEnded += PlayerOnPlaybackEnded;

        Play();
    }
    
    public void Play()
    {
        if (_player == null || _mediaInfo == null)
            throw new NullReferenceException();
        
        _player.Play();
        
        _mediaInfoManager.SetMediaInfo(_mediaInfo);
        _mediaInfoManager.SetPlayerInfo(PlayerInfo);
    }

    public void Pause()
    {
        if (_player == null)
            throw new NullReferenceException();
        
        _player.Pause();
        _mediaInfoManager.SetPlayerInfo(PlayerInfo);
    }

    public void Seek(double positionInSeconds)
    {
        _player?.Seek(positionInSeconds);
        _mediaInfoManager.SetPlayerInfo(PlayerInfo);
    }
    
    private void Stop()
    {
        if (_player != null)
            _player.PlaybackEnded -= PlayerOnPlaybackEnded;
        
        _player?.Dispose();
        _player = null;
    }
    
    private void PlayerOnPlaybackEnded(object? sender, EventArgs e)
    {
        PlaybackEnded?.Invoke(this, EventArgs.Empty);
    }
}