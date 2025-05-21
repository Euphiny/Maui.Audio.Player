using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Media.Session;
using Android.Support.V4.Media;
using Android.Support.V4.Media.Session;
using Bumptech.Glide;
using Bumptech.Glide.Request.Target;
using Bumptech.Glide.Request.Transition;
using Object = Java.Lang.Object;

namespace Maui.Audio.Player.MediaInfoManager;

public partial class MediaInfoManager : IMediaInfoManager
{
    private const string SessionTag = "Maui.Audio.Player.MediaInfoManager";

    private static bool _serviceIsInitialized;
    
    private static MediaSessionCompat? _mediaSession;
    public static MediaSessionCompat? MediaSession => _mediaSession;
    
    private readonly MediaSessionCallback _mediaSessionCallback = new();
    
    public MediaInfoManager()
    {
        _mediaSession = new MediaSessionCompat(Android.App.Application.Context, SessionTag);
        
        _mediaSession.SetCallback(_mediaSessionCallback);
        
#pragma warning disable CS0618 // Type or member is obsolete
        _mediaSession.SetFlags(MediaSessionCompat.FlagHandlesMediaButtons | MediaSessionCompat.FlagHandlesTransportControls);
#pragma warning restore CS0618 // Type or member is obsolete
    }
    
    public void Initialize()
    {
        
    }

    public void SetMediaInfo(MediaInfo mediaInfo)
    {
        var metadata = BuildMediaMetaData(mediaInfo);
        
        if (_mediaSession == null)
            return;
        
        _mediaSession.SetMetadata(metadata);
        _mediaSession.Active = true;

        if (_serviceIsInitialized)
        {
            var notification = MediaNotificationManager.Instance.CreateNotification(_mediaSession);
            MediaNotificationManager.Instance.ShowNotification(notification);
            return;
        }

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
            ?.SetState(playState, playerInfo.CurrentProgress.Milliseconds, 1f)
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
    
    private static MediaMetadataCompat? BuildMediaMetaData(MediaInfo mediaInfo)
    {
        var metadata = new MediaMetadataCompat.Builder()
            .PutString(MediaMetadataCompat.MetadataKeyTitle, mediaInfo.Title)
            ?.PutString(MediaMetadataCompat.MetadataKeyArtist, mediaInfo.Artist)
            ?.PutLong(MediaMetadataCompat.MetadataKeyDuration, (long)mediaInfo.TotalDuration.TotalMilliseconds);

        var imageUrl = mediaInfo.ImageUrl;

        if (string.IsNullOrEmpty(imageUrl)) 
            return metadata?.Build();
        
        var bitmap = Task.Run(async () =>
        {
            try
            {
                using var httpClient = new HttpClient();
                var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                return BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading bitmap: {ex.Message}");
                return null;
            }
        }).GetAwaiter().GetResult();
            
        metadata?.PutBitmap(MediaMetadataCompat.MetadataKeyAlbumArt, bitmap);

        return metadata?.Build();
    }
}
