using Maui.Audio.Player.MediaInfoManager;

namespace Maui.Audio.Player;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseMauiAudioPlayer(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<IMediaInfoManager, MediaInfoManager.MediaInfoManager>();
        
        return builder;
    }
}