namespace Maui.Audio.Player;

public class MediaInfo(string title, string artist, double duration, string? imageUrl = null)
{
    public string Title { get; set; } = title;

    public string Artist { get; set; } = artist;

    public double Duration { get; set; } = duration;

    public string? ImageUrl { get; set; } = imageUrl;
}