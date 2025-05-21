using Android.Media;
using Application = Android.App.Application;
using Uri = Android.Net.Uri;

namespace Maui.Audio.Player.AudioPlayer;

public partial class AudioPlayer : IAudioPlayer
{
    private readonly MediaPlayer _mediaPlayer;
    
    private bool _isDisposed;
    
    [Obsolete("Use CurrentProgress instead.")]
    public double Progress => _mediaPlayer.CurrentPosition / 1000d;
    [Obsolete("Use TotalDuration instead.")]
    public double Duration => TotalDuration.TotalSeconds;

    public TimeSpan CurrentProgress => TimeSpan.FromMilliseconds(_mediaPlayer.CurrentPosition);
    public TimeSpan TotalDuration { get; }
    public bool IsPlaying => _mediaPlayer.IsPlaying;

    [Obsolete("Use overload with TimeSpan duration instead.")]
    public AudioPlayer(string url, double duration) : this(url, TimeSpan.FromSeconds(duration))
    {
        
    }

    public AudioPlayer(string url, TimeSpan duration)
    {
        TotalDuration = duration;
        _mediaPlayer = new MediaPlayer();

        var uri = Uri.Parse(url);
        if (uri == null)
            throw new ArgumentException("Invalid url");

        _mediaPlayer.Completion += MediaPlayerOnCompletion;
        
        _mediaPlayer.SetDataSource(Application.Context, uri);
        _mediaPlayer.Prepare();
    }

    public void Play()
    {
        _mediaPlayer.Start();
    }

    public void Pause()
    {
        _mediaPlayer.Pause();
    }

    public void Seek(double position)
    {
        _mediaPlayer.SeekTo((int)position * 1000);
    }

    public void Dispose(bool disposing)
    {
        if (_isDisposed)
            return;

        if (disposing)
        {
            _mediaPlayer.Completion -= MediaPlayerOnCompletion;
            
            _mediaPlayer.Stop();
            _mediaPlayer.Dispose();
        }
        
        _isDisposed = true;
    }
    
    private void MediaPlayerOnCompletion(object? sender, EventArgs e)
    {
        PlaybackEnded?.Invoke(this, EventArgs.Empty);
    }
}