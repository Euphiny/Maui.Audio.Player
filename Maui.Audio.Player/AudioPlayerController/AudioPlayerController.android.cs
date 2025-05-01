namespace Maui.Audio.Player.AudioPlayerController;

public partial class AudioPlayerController : IAudioPlayerController
{
    private AudioPlayer.AudioPlayer? _player;

    public PlayerInfo PlayerInfo => new(_player?.Duration ?? 0, _player?.Progress ?? 0, _player?.IsPlaying ?? false);
    
    public void Start(string url, MediaInfo mediaInfo)
    {
        Stop();
        
        _player = new AudioPlayer.AudioPlayer(url, mediaInfo.Duration);
        _player.PlaybackEnded += PlayerOnPlaybackEnded;

        Play();
    }

    public void Play()
    {
        if (_player == null)
            throw new NullReferenceException();
        
        _player.Play();
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