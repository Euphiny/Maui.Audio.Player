using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.Media;
using Android.Support.V4.Media.Session;
using AndroidX.Core.App;
using MediaSession = Android.Media.Session.MediaSession;
using MediaStyle = AndroidX.Media.App.NotificationCompat.MediaStyle;

namespace Maui.Audio.Player.MediaInfoManager;

public class MediaNotificationManager
{
    private const string ChannelId = "audio_player_channel";

    private static bool _isInitialized;
    
    public void CreateNotificationChannel()
    {
        if (!OperatingSystem.IsAndroidVersionAtLeast(26) || _isInitialized)
            return;
        
        _isInitialized = true;
        
        var channel = new NotificationChannel(ChannelId, "Audio Player", NotificationImportance.Default)
        {
            Description = "Play music"
        };
        
        var manager = Platform.AppContext.GetSystemService(Context.NotificationService) as NotificationManager;
        manager?.CreateNotificationChannel(channel);
    }
    
    public Notification CreateNotification(MediaSessionCompat mediaSession)
    {
        var innerSession = mediaSession.MediaSession as MediaSession;
        var metadata = innerSession?.Controller.Metadata;
        
        if (metadata == null)
            throw new NullReferenceException("Metadata of media session could not be null.");

        var mediaStyle = new MediaStyle()
            .SetMediaSession(mediaSession.SessionToken)
            ?.SetShowActionsInCompactView(0);
        
        var builder = new NotificationCompat.Builder(Platform.AppContext, ChannelId)
            .SetContentIntent(mediaSession.Controller?.SessionActivity)
            ?.SetContentTitle(metadata.GetString(MediaMetadataCompat.MetadataKeyTitle))
            ?.SetContentText(metadata.GetString(MediaMetadataCompat.MetadataKeyArtist))
            ?.SetLargeIcon(BitmapFactory.DecodeResource(Platform.AppContext.Resources, Resource.Drawable.abc_ic_go_search_api_material))
            .SetSmallIcon(Resource.Drawable.abc_ic_go_search_api_material)
            .SetStyle(mediaStyle);

        return builder.Build();
    }
}