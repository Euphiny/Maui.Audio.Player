namespace Maui.Audio.Player.MediaInfoManager;

public partial class MediaInfoManager : IMediaInfoManager
{
    public void Initialize() { }

    public void SetMediaInfo(MediaInfo mediaInfo) { }

    public void SetPauseCommand(Action action) { }

    public void SetPlayCommand(Action action) { }
    
    public void SetNextCommand(Action action) { }

    public void SetPreviousCommand(Action action) { }
}