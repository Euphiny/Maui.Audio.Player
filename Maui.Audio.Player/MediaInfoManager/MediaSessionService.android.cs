using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Media;
using Android.Support.V4.Media.Session;
using AndroidX.Core.App;
using AndroidX.Media;

namespace Maui.Audio.Player.MediaInfoManager;

[Service(ForegroundServiceType = ForegroundService.TypeMediaPlayback, Exported = true, Enabled = true)]
[IntentFilter([Android.Service.Media.MediaBrowserService.ServiceInterface])]
public class MediaSessionService : MediaBrowserServiceCompat
{
    private const string SessionTag = "Maui.Audio.Player.MediaInfoManager";
    private const string ChannelId = "audio_player_channel";
    
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

        _mediaSession = MediaInfoManager.MediaSession;
        SessionToken = _mediaSession?.SessionToken;
        
        CreateNotificationChannel();
        
        NotificationCompat.Builder builder = new NotificationCompat.Builder(Platform.AppContext,"audio_player_channel")
            .SetContentIntent(_mediaSession.Controller.SessionActivity)
            .SetContentTitle("Title of song")
            .SetContentText("Message")
            .SetLargeIcon(BitmapFactory.DecodeResource(Platform.AppContext.Resources, Resource.Drawable.abc_ic_go_search_api_material))
            .SetSmallIcon(Resource.Drawable.abc_ic_go_search_api_material)
            .SetStyle(new AndroidX.Media.App.NotificationCompat.MediaStyle()
                .SetMediaSession(_mediaSession.SessionToken)
                .SetShowActionsInCompactView(0));

        StartForeground(1, builder.Build());
        var notification = builder.Build();
        var compatManager = NotificationManagerCompat.From(Platform.AppContext);
        compatManager.Notify(1, notification);
    }
    
    private static void CreateNotificationChannel()
    {
        if (!OperatingSystem.IsAndroidVersionAtLeast(26))
            return;
        
        var channel = new NotificationChannel(ChannelId, "Audio Player", NotificationImportance.Default)
        {
            Description = "Play music"
        };
        
        var manager = Platform.AppContext.GetSystemService(NotificationService) as NotificationManager;
        manager?.CreateNotificationChannel(channel);
    }
}
