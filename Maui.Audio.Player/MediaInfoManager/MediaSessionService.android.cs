using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Media;
using Android.Support.V4.Media.Session;
using AndroidX.Core.App;
using AndroidX.Media;
using MediaSession = Android.Media.Session.MediaSession;
using MediaStyle = AndroidX.Media.App.NotificationCompat.MediaStyle;

namespace Maui.Audio.Player.MediaInfoManager;

[Service(ForegroundServiceType = ForegroundService.TypeMediaPlayback, Exported = true, Enabled = true)]
[IntentFilter([Android.Service.Media.MediaBrowserService.ServiceInterface])]
public class MediaSessionService : MediaBrowserServiceCompat
{
    private MediaSessionCompat? _mediaSession;

    private MediaNotificationManager _notificationManager = new();
    
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

        _mediaSession = MediaInfoManager.MediaSession;
        SessionToken = _mediaSession?.SessionToken;
        
        _notificationManager.CreateNotificationChannel();
        var notification = _notificationManager.CreateNotification(_mediaSession!);

        StartForeground(1, notification);
        var compatManager = NotificationManagerCompat.From(Platform.AppContext);
        compatManager?.Notify(1, notification);
    }
}
