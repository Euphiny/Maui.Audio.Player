using Maui.Audio.Player.MediaInfoManager;

namespace Maui.Audio.Player.AudioPlayerController;

public partial class AudioPlayerController : IAudioPlayerController
{
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
        _player.PlaybackEnded += PlayerOnPlaybackEnded;

        Play();
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