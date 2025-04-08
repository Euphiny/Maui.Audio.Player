namespace Maui.Audio.Player.AudioPlayer;

public interface IAudioPlayer : IDisposable
{
    public void Play();

    public void Pause();
}