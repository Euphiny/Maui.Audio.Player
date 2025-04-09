#if IOS
    using AVFoundation;
#endif
using Maui.Audio.Player.MediaInfoManager;

namespace Maui.Audio.Player;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseMauiAudioPlayer(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<IMediaInfoManager, MediaInfoManager.MediaInfoManager>();
        
        #if IOS
        // TODO: move this to a proper initialization class or something
            var audioSession = AVAudioSession.SharedInstance();
        
            audioSession.SetCategory(AVAudioSessionCategory.Playback);
            audioSession.SetActive(true);
        #endif
        
        return builder;
    }
}