using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Media;

namespace Maui.Audio.Player.MediaInfoManager;

[Service(ForegroundServiceType = ForegroundService.TypeMediaPlayback, Exported = true, Enabled = true)]
public class MediaSessionService : MediaBrowserServiceCompat
{
    public override BrowserRoot OnGetRoot(string clientPackageName, int clientUid, Bundle rootHints)
        => new (nameof(ApplicationContext.ApplicationInfo.Name), null);

    public override void OnLoadChildren(string parentId, Result result)
    {
        
    }
}