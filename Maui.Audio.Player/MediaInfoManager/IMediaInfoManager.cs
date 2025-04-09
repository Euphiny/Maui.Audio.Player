namespace Maui.Audio.Player.MediaInfoManager;

public interface IMediaInfoManager
{
    public void Initialize();
    
    public void SetMediaInfo(MediaInfo mediaInfo);
}