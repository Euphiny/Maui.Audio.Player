using Android.Media.Session;
using Application = Android.App.Application;

namespace Maui.Audio.Player.MediaInfoManager;

public partial class MediaInfoManager : IMediaInfoManager
{
    private const string SessionTag = "Maui.Audio.Player.MediaInfoManager";
    
    private MediaSession _mediaSession;
    
    public MediaInfoManager()
    {
        _mediaSession = new MediaSession(Application.Context, SessionTag);
        
        _mediaSession.SetCallback(new MediaSessionCallback());
        _mediaSession.SetFlags(MediaSessionFlags.HandlesMediaButtons | MediaSessionFlags.HandlesTransportControls);
    }
    
    public void Initialize()
    {
        
    }

    public void SetMediaInfo(MediaInfo mediaInfo)
    {
        
    }

    public void SetPauseCommand(Action action)
    {
        
    }

    public void SetPlayCommand(Action action)
    {
        
    }

    public void SetNextCommand(Action action)
    {
        
    }

    public void SetPreviousCommand(Action action)
    {
        
    }
}

public class MediaSessionCallback : MediaSession.Callback;