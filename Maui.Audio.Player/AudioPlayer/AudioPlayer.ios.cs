using AVFoundation;
using Foundation;

namespace Maui.Audio.Player.AudioPlayer;

public partial class AudioPlayer : IAudioPlayer
{
    private bool _isDisposed;
    
    private readonly AVPlayer _player;

    public double Progress => _player.CurrentTime.Seconds;
    public double Duration => _player.CurrentItem?.Duration.Seconds ?? 0;

    public AudioPlayer(string url, double duration)
    {
        var nsUrl = new NSUrl(url);
        _player = new AVPlayer(nsUrl);
    }
    
    public void Play()
    {
        _player.Play();
    }

    public void Pause()
    {
        _player.Pause();
    }

    public void Dispose(bool disposing)
    {
        if (_isDisposed)
            return;

        if (disposing)
        {
            _player.Dispose();
        }
        
        _isDisposed = true;
    }
}