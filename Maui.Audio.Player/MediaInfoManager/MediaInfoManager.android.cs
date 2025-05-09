using Android.Content;
using Android.Media.Session;
using Android.Support.V4.Media;
using Android.Support.V4.Media.Session;
using MediaSession = Android.Media.Session.MediaSession;

namespace Maui.Audio.Player.MediaInfoManager;

public partial class MediaInfoManager : IMediaInfoManager
{
    private const string SessionTag = "Maui.Audio.Player.MediaInfoManager";

    private static bool _serviceIsInitialized = false;
    
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
        
        if (!_serviceIsInitialized)
            Android.App.Application.Context.StartForegroundService(new Intent(Android.App.Application.Context, typeof(MediaSessionService)));

        _serviceIsInitialized = true;
        _mediaSession.SetMetadata(metadata);
        _mediaSession.SetPlaybackState(stateBuilder.Build());
        
        _mediaSession.Active = true;
        
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
}

public class MediaSessionCallback : MediaSessionCompat.Callback
{
    public override void OnPlay()
    {
        base.OnPlay();

        var t = " ";
    }
}