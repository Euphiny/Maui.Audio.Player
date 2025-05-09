namespace Maui.Audio.Player.MediaInfoManager;

public interface IMediaInfoManager
{
    public void Initialize();
    
    public void SetMediaInfo(MediaInfo mediaInfo);
    public void SetPlayerInfo(PlayerInfo playerInfo);

    public void SetPauseCommand(Action action);
    public void SetPlayCommand(Action action);
    
    public void SetNextCommand(Action action);
    public void SetPreviousCommand(Action action);
}