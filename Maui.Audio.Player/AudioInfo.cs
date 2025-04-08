namespace Maui.Audio.Player;

public class AudioInfo(string title, double durationInSeconds)
{
    public string Title { get; set; } = title;
    
    public double DurationInSeconds { get; set; } = durationInSeconds;
}