using Android.Media;
using Application = Android.App.Application;
using Uri = Android.Net.Uri;

namespace Maui.Audio.Player.AudioPlayer;

public partial class AudioPlayer : IAudioPlayer
{
    private readonly MediaPlayer _mediaPlayer;
    
    private bool _isDisposed;
    
    public double Progress => _mediaPlayer.CurrentPosition;
    public double Duration { get; }
    public bool IsPlaying => _mediaPlayer.IsPlaying;

    public AudioPlayer(string url, double duration)
    {
        Duration = duration;
        _mediaPlayer = new MediaPlayer();

        var uri = Uri.Parse(url);
        if (uri == null)
            throw new ArgumentException("Invalid url");
        
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
        throw new NotImplementedException();
    }

    public async Task SeekAsync(double position)
    {
        throw new NotImplementedException();
    }

    public void Dispose(bool disposing)
    {
        if (_isDisposed)
            return;

        if (disposing)
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Dispose();
        }
        
        _isDisposed = true;
    }
}