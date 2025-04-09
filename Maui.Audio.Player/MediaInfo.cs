namespace Maui.Audio.Player;

public class MediaInfo(string title, string artist)
{
    public string Title { get; set; } = title;

    public string Artist { get; set; } = artist;
}