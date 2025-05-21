namespace Maui.Audio.Player;

public class MediaInfo(string title, string artist, TimeSpan duration, string? imageUrl = null)
{
    public string Title { get; set; } = title;

    public string Artist { get; set; } = artist;

    [Obsolete("Use TotalDuration instead")]
    public double Duration { get; set; } = duration.TotalSeconds;

    public TimeSpan TotalDuration { get; set; } = duration;

    public string? ImageUrl { get; set; } = imageUrl;

    [Obsolete("Use the overload with TimeSpan duration instead.")]
    public MediaInfo(string title, string artist, double duration, string? imageUrl = null) 
        : this(title, artist, TimeSpan.FromSeconds(duration), imageUrl) { }
}