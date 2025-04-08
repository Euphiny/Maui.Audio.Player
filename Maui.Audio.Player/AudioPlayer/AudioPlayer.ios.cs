using AVFoundation;
using Foundation;

namespace Maui.Audio.Player.AudioPlayer;

public partial class AudioPlayer
{
    private AVPlayer _player;

    public AudioPlayer(string url)
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
    
    public void Dispose(bool disposing) {}
}