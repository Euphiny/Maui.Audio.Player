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

    public void SetPlayerInfo(PlayerInfo playerInfo)
    {
        throw new NotImplementedException();
    }

    public void SetPauseCommand(Action action)
    {
        MPRemoteCommandCenter.Shared.PauseCommand.AddTarget(HandleCommand(action));
    }

    public void SetPlayCommand(Action action)
    {
        MPRemoteCommandCenter.Shared.PlayCommand.AddTarget(HandleCommand(action));
    }

    public void SetNextCommand(Action action)
    {
        MPRemoteCommandCenter.Shared.NextTrackCommand.AddTarget(HandleCommand(action));
    }

    public void SetPreviousCommand(Action action)
    {
        MPRemoteCommandCenter.Shared.PreviousTrackCommand.AddTarget(HandleCommand(action));
    }

    private Func<MPRemoteCommandEvent, MPRemoteCommandHandlerStatus> HandleCommand(Action action)
    {
        return _ =>
        {
            try
            {
                action();

                return MPRemoteCommandHandlerStatus.Success;
            }
            catch
            {
                return MPRemoteCommandHandlerStatus.CommandFailed;
            }
        };
    }
}