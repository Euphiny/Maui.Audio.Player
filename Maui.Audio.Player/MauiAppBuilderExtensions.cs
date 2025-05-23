using Maui.Audio.Player.AudioPlayerController;
using Maui.Audio.Player.MediaInfoManager;

namespace Maui.Audio.Player;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseMauiAudioPlayer(this MauiAppBuilder builder, AudioPlayerOptions? options = null)
    {
        builder.Services.AddSingleton<IMediaInfoManager, MediaInfoManager.MediaInfoManager>();
        builder.Services.AddSingleton<IAudioPlayerController, AudioPlayerController.AudioPlayerController>();

        #if ANDROID
        MediaNotificationManager.Instance.Options = options;
        #endif
        
        return builder;
    }
}