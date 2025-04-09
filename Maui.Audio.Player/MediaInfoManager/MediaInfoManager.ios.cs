using MediaPlayer;

namespace Maui.Audio.Player.MediaInfoManager;

public partial class MediaInfoManager : IMediaInfoManager
{
    public void SetMediaInfo(MediaInfo mediaInfo)
    {
        MPNowPlayingInfoCenter.DefaultCenter.NowPlaying = new MPNowPlayingInfo
        {
            Title = mediaInfo.Title,
            Artist = mediaInfo.Artist,
            PlaybackDuration = mediaInfo.Duration,
            ElapsedPlaybackTime = 0,
            PlaybackRate = 1.0f
        };
    }
}