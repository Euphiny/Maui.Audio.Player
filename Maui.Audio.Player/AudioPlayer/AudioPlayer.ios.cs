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
    public double Duration => TotalDuration.TotalSeconds;
    
    public TimeSpan CurrentProgress => TimeSpan.FromSeconds(_player.CurrentTime.Seconds);

    public TimeSpan TotalDuration
    {
        get
        {
            var duration = _player.CurrentItem?.Asset.Duration.Seconds ?? 0d;

            if (!double.IsNaN(duration))
                return TimeSpan.FromSeconds(duration);
            
            return TimeSpan.FromSeconds(0);
        }
    }

    public bool IsPlaying => _player.TimeControlStatus == AVPlayerTimeControlStatus.Playing;

    [Obsolete("Use overload which only takes the url as input.")]
    public AudioPlayer(string url, double duration) : this(url, TimeSpan.FromSeconds(duration))
    {
        
    }

    [Obsolete("Use overload which only takes the url as input.")]
    public AudioPlayer(string url, TimeSpan duration) : this(new Uri(url))
    {

    }

    public AudioPlayer(Uri url)
    {
        var nsUrl = new NSUrl(url.AbsoluteUri);
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
        if (Math.Floor(CurrentProgress.TotalSeconds) >= Math.Floor(TotalDuration.TotalSeconds))
        {
            _player.RemoveTimeObserver(_playbackStoppedObserver);
            PlaybackEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}