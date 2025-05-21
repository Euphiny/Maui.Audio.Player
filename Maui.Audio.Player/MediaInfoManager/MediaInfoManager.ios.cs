using AVFoundation;
using CoreGraphics;
using Foundation;
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
        var nowPlayingInfo = new MPNowPlayingInfo
        {
            Title = mediaInfo.Title,
            Artist = mediaInfo.Artist,
            PlaybackDuration = mediaInfo.TotalDuration.TotalSeconds,
            ElapsedPlaybackTime = 0,
            PlaybackRate = 1.0f
        };

        var imageUrl = mediaInfo.ImageUrl;
        
        if (!string.IsNullOrEmpty(imageUrl))
        {
            var fromBundle = UIImage.FromBundle(imageUrl);
            
            if (fromBundle != null)
                nowPlayingInfo.Artwork = new MPMediaItemArtwork(new CGSize(fromBundle.Size.Width, fromBundle.Size.Height), _ => fromBundle);
        }

        MPNowPlayingInfoCenter.DefaultCenter.NowPlaying = nowPlayingInfo;
    }

    public void SetPlayerInfo(PlayerInfo playerInfo)
    {
        MPNowPlayingInfoCenter.DefaultCenter.NowPlaying.ElapsedPlaybackTime = (float)playerInfo.CurrentProgress.TotalSeconds;
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