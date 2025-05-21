using AVFoundation;
using CoreMedia;
using Foundation;

namespace Maui.Audio.Player.AudioPlayer;

public partial class AudioPlayer : IAudioPlayer
{
    private bool _isDisposed;
    
    private readonly AVPlayer _player;
    private readonly NSObject _playbackStoppedObserver;

    [Obsolete("Use CurrentProgress instead.")]
    public double Progress => _player.CurrentTime.Seconds;
    [Obsolete("Use TotalDuration instead.")]
    public double Duration { get; }
    
    public TimeSpan CurrentProgress => TimeSpan.FromSeconds(_player.CurrentTime.Seconds);
    public TimeSpan TotalDuration { get; }

    public bool IsPlaying => _player.TimeControlStatus == AVPlayerTimeControlStatus.Playing;

    public AudioPlayer(string url, double duration)
    {
        Duration = duration;
        
        var nsUrl = new NSUrl(url);
        _player = new AVPlayer(nsUrl);

        _playbackStoppedObserver = _player.AddPeriodicTimeObserver(CMTime.FromSeconds(1, 1), null, CheckPlaybackStopped);
    }
    
    public void Play()
    {
        _player.Play();
    }

    public void Pause()
    {
        _player.Pause();
    }

    public void Seek(double position)
    {
        _player.Seek(CMTime.FromSeconds(position, 1));
    }

    public void Dispose(bool disposing)
    {
        if (_isDisposed)
            return;

        if (disposing)
        {
            _player.Dispose();
            _player.RemoveTimeObserver(_playbackStoppedObserver);
        }
        
        _isDisposed = true;
    }

    private void CheckPlaybackStopped(CMTime time)
    {
        if (Math.Floor(Progress) >= Math.Floor(Duration))
        {
            _player.RemoveTimeObserver(_playbackStoppedObserver);
            PlaybackEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}