namespace Maui.Audio.Player;

public class MediaInfo(string title, string artist, double duration)
{
    public string Title { get; set; } = title;

    public string Artist { get; set; } = artist;
    
    public double Duration { get; set; }
}