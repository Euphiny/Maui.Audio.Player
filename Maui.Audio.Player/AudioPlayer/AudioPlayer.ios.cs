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
        throw new NotImplementedException();
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }
}