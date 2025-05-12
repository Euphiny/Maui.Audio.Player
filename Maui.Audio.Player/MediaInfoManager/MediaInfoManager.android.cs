using Android.Content;
using Android.Media.Session;
using Android.Support.V4.Media;
using Android.Support.V4.Media.Session;

namespace Maui.Audio.Player.MediaInfoManager;

public partial class MediaInfoManager : IMediaInfoManager
{
    private const string SessionTag = "Maui.Audio.Player.MediaInfoManager";

    private static bool _serviceIsInitialized;
    private static int _pendingIntentId = 0;
    
    private readonly MediaSessionCallback _mediaSessionCallback = new();
    
    private static MediaSessionCompat? _mediaSession;
    
    public static MediaSessionCompat? MediaSession => _mediaSession;
    
    public MediaInfoManager()
    {
        _mediaSession = new MediaSessionCompat(Android.App.Application.Context, SessionTag);
        
        _mediaSession.SetCallback(_mediaSessionCallback);
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

        if (_mediaSession == null)
            return;
        
        _mediaSession.SetMetadata(metadata);
        _mediaSession.Active = true;
        
        if (!_serviceIsInitialized)
            Android.App.Application.Context.StartForegroundService(new Intent(Android.App.Application.Context, typeof(MediaSessionService)));

        _serviceIsInitialized = true;
    }

    public void SetPlayerInfo(PlayerInfo playerInfo)
    {
        var playState = playerInfo.IsPlaying ? PlaybackStateCompat.StatePlaying : PlaybackStateCompat.StatePaused;
        
        var state = new PlaybackStateCompat.Builder()
            .SetActions(
                PlaybackStateCompat.ActionPlay |
                PlaybackStateCompat.ActionPlayPause |
                PlaybackState.ActionSkipToNext |
                PlaybackState.ActionSkipToPrevious)
            ?.SetState(playState, (long)playerInfo.Progress * 1000, 1f)
            ?.Build();
        
        _mediaSession?.SetPlaybackState(state);
    }

    public void SetPauseCommand(Action action)
    {
        _mediaSessionCallback.OnPauseCommand = action;
    }

    public void SetPlayCommand(Action action)
    {
        _mediaSessionCallback.OnPlayCommand = action;
    }

    public void SetNextCommand(Action action)
    {
        _mediaSessionCallback.OnSkipToNextCommand = action;
    }

    public void SetPreviousCommand(Action action)
    {
        _mediaSessionCallback.OnSkipToPreviousCommand = action;
    }
}
