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

public class MediaSessionCallback : MediaSessionCompat.Callback
{
    private readonly MediaNotificationManager _notificationManager = new();
    
    public Action? OnPlayCommand { get; set; }
    public Action? OnPauseCommand { get; set; }
    public Action? OnSkipToNextCommand { get; set; }
    public Action? OnSkipToPreviousCommand { get; set; }
    
    public override void OnPlay()
    {
        OnPlayCommand?.Invoke();
    }

    public override void OnPause()
    {
        OnPauseCommand?.Invoke();
    }

    public override void OnSkipToNext()
    {
        OnSkipToNextCommand?.Invoke();
    }

    public override void OnSkipToPrevious()
    {
        OnSkipToPreviousCommand?.Invoke();
    }
}