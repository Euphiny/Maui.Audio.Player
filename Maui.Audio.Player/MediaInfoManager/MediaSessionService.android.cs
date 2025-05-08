using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Media;
using Android.Support.V4.Media.Session;
using AndroidX.Media;

namespace Maui.Audio.Player.MediaInfoManager;

[Service(ForegroundServiceType = ForegroundService.TypeMediaPlayback, Exported = true, Enabled = true)]
public class MediaSessionService : MediaBrowserServiceCompat
{
    private const string SessionTag = "Maui.Audio.Player.MediaInfoManager";
    
    private MediaSessionCompat? _mediaSession;
    
    public override BrowserRoot OnGetRoot(string clientPackageName, int clientUid, Bundle rootHints)
        => new (nameof(ApplicationContext.ApplicationInfo.Name), null);

    public override void OnLoadChildren(string parentId, Result result)
    {
        var mediaItems = new JavaList<MediaBrowserCompat.MediaItem>();

        result.SendResult(mediaItems);
    }

    public override void OnCreate()
    {
        base.OnCreate();
        
        _mediaSession = new MediaSessionCompat(Android.App.Application.Context, SessionTag);
        
        _mediaSession.SetCallback(new MediaSessionCompatCallback());
        _mediaSession.SetFlags(MediaSessionCompat.FlagHandlesMediaButtons |
                               MediaSessionCompat.FlagHandlesTransportControls);

        SessionToken = _mediaSession.SessionToken;
    }
}

public class MediaSessionCompatCallback : MediaSessionCompat.Callback;