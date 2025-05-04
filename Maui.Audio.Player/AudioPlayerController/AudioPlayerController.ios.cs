using Maui.Audio.Player.MediaInfoManager;

namespace Maui.Audio.Player.AudioPlayerController;

public partial class AudioPlayerController : IAudioPlayerController
{
    public PlayerInfo PlayerInfo => new(_player?.Duration ?? 0, _player?.Progress ?? 0, _player?.IsPlaying ?? false);
    
    public AudioPlayerController(IMediaInfoManager mediaInfoManager)
    {
        _mediaInfoManager = mediaInfoManager;

        _mediaInfoManager.SetPauseCommand(Pause);
        _mediaInfoManager.SetPlayCommand(Play);
    }

    public void Start(string url, MediaInfo mediaInfo)
    {
        Stop();
        
        _mediaInfo = mediaInfo;
        
        _player = new AudioPlayer.AudioPlayer(url, mediaInfo.Duration);
        _player.PlaybackEnded += PlayerPlaybackEnded;
        
        Play();
    }

    private void Stop()
    {
        if (_player == null)
            return;
        
        _player.PlaybackEnded -= PlayerPlaybackEnded;
        _player.Dispose();
    }
    
    private void PlayerPlaybackEnded(object? sender, EventArgs eventArgs)
    {
        Stop();
        
        PlaybackEnded?.Invoke(this, EventArgs.Empty);
    }
}