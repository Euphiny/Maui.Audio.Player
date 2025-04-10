namespace Maui.Audio.Player;

public class PlayerInfo
{
    public double Duration { get; }
    
    public double Progress { get; }
    
    public bool IsPlaying { get; }
    
    internal PlayerInfo(double duration, double progress, bool isPlaying)
    {
        Duration = duration;
        Progress = progress;
        IsPlaying = isPlaying;
    }
}