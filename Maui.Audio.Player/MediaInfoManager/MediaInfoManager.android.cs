using System.Diagnostics;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media.Session;
using Android.OS;
using Android.Support.V4.Media;
using Android.Support.V4.Media.Session;
using AndroidX.Core.App;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Platform;

namespace Maui.Audio.Player.MediaInfoManager;

public partial class MediaInfoManager : IMediaInfoManager
{
    private const string ChannelId = "audio_player_channel";
    private const string SessionTag = "Maui.Audio.Player.MediaInfoManager";

    private static bool _serviceIsInitialized = false;
    private static bool _channelInitialized = false;
    private static int _pendingIntentId = 0;
    
    private static MediaSessionCompat? _mediaSession;
    
    public static MediaSessionCompat? MediaSession => _mediaSession;
    
    public MediaInfoManager()
    {
        _mediaSession = new MediaSessionCompat(Android.App.Application.Context, SessionTag);
        
        _mediaSession.SetCallback(new MediaSessionCallback());
        _mediaSession.SetFlags(MediaSessionCompat.FlagHandlesMediaButtons | MediaSessionCompat.FlagHandlesTransportControls);
    }
    
    public void Initialize()
    {
        
    }

    public void SetMediaInfo(MediaInfo mediaInfo)
    {
        var metadata = new MediaMetadataCompat.Builder()
            .PutString(MediaMetadataCompat.MetadataKeyTitle, mediaInfo.Title)
            ?.PutString(MediaMetadataCompat.MetadataKeyArtist, mediaInfo.Artist)
            ?.Build();
        
        var stateBuilder = new PlaybackStateCompat.Builder()
            .SetActions(
                PlaybackStateCompat.ActionPlay |
                PlaybackStateCompat.ActionPlayPause);

        if (_mediaSession == null)
            return;
        
        CreateNotificationChannel();
        
        _mediaSession.SetMetadata(metadata);
        _mediaSession.SetPlaybackState(stateBuilder.Build());
        
        _mediaSession.Active = true;
        
        if (!_serviceIsInitialized)
            Android.App.Application.Context.StartForegroundService(new Intent(Android.App.Application.Context, typeof(MediaSessionService)));

        _serviceIsInitialized = true;
        _mediaSession.Controller.GetTransportControls().Play();
    }

    public void SetPlayerInfo(PlayerInfo playerInfo)
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

    private void CreateNotificationChannel()
    {
        if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            return;
        
        var channelNameJava = new Java.Lang.String("Audio Player");
        var channel = new NotificationChannel(ChannelId, channelNameJava, NotificationImportance.Default)
        {
            Description = "Play music"
        };
        
        NotificationManager manager = (NotificationManager)Platform.AppContext.GetSystemService(Context.NotificationService);
        manager.CreateNotificationChannel(channel);
        _channelInitialized = true;
    }
}

public class MediaSessionCallback : MediaSessionCompat.Callback
{
    public override void OnPlay()
    {
        base.OnPlay();

        var t = " ";
    }
}