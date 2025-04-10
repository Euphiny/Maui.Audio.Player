using AVFoundation;
using MediaPlayer;
using UIKit;

namespace Maui.Audio.Player.MediaInfoManager;

public partial class MediaInfoManager : IMediaInfoManager
{
    public void Initialize()
    {
        var audioSession = AVAudioSession.SharedInstance();
        
        audioSession.SetCategory(AVAudioSessionCategory.Playback);
        audioSession.SetActive(true);
            
        UIApplication.SharedApplication.BeginReceivingRemoteControlEvents();
    }

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

    public void SetPauseCommand(Action action)
    {
        throw new NotImplementedException();
    }

    public void SetPlayCommand(Action action)
    {
        throw new NotImplementedException();
    }
}