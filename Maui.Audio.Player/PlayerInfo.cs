namespace Maui.Audio.Player;

public class PlayerInfo
{
    [Obsolete("Use CurrentProgress instead.")]
    public double Progress => CurrentProgress.TotalSeconds;
    [Obsolete("Use TotalDuration instead.")]
    public double Duration => TotalDuration.TotalSeconds;
    
    public TimeSpan CurrentProgress { get; }
    public TimeSpan TotalDuration { get; }
    
    public bool IsPlaying { get; }
    
    internal PlayerInfo(TimeSpan? duration, TimeSpan? progress, bool isPlaying)
    {
        TotalDuration = duration ?? TimeSpan.Zero;
        CurrentProgress = progress ?? TimeSpan.Zero;
        IsPlaying = isPlaying;
    }
}