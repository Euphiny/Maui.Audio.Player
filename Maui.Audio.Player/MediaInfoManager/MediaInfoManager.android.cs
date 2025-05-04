using Android.Media;
using Android.Media.Session;
using Application = Android.App.Application;

namespace Maui.Audio.Player.MediaInfoManager;

public partial class MediaInfoManager : IMediaInfoManager
{
    private const string SessionTag = "Maui.Audio.Player.MediaInfoManager";
    
    private readonly MediaSession _mediaSession;
    
    public MediaInfoManager()
    {
        _mediaSession = new MediaSession(Application.Context, SessionTag);
        
        _mediaSession.SetCallback(new MediaSessionCallback());
        _mediaSession.SetFlags(MediaSessionFlags.HandlesMediaButtons | MediaSessionFlags.HandlesTransportControls);
    }
    
    public void Initialize()
    {
        
    }

    public void SetMediaInfo(MediaInfo mediaInfo)
    {
        var metadata = new MediaMetadata.Builder()
            .PutString(MediaMetadata.MetadataKeyTitle, mediaInfo.Title)
            ?.PutString(MediaMetadata.MetadataKeyArtist, mediaInfo.Artist)
            ?.Build();
        
        var playbackState = new PlaybackState.Builder()
            .SetActions(PlaybackState.ActionPlay)
            ?.SetState(PlaybackStateCode.Playing, PlaybackState.PlaybackPositionUnknown, 1)
            ?.Build();
        
        _mediaSession.SetMetadata(metadata);
        _mediaSession.SetPlaybackState(playbackState);
        
        _mediaSession.Active = true;
    }

    public void SetPlayerInfo(PlayerInfo playerInfo)
    {
        throw new NotImplementedException();
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

public class MediaSessionCallback : MediaSession.Callback;