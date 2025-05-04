# Maui.Audio.Player

A package which allows you to play audio in your MAUI application. Either use it as standalone solution or integrate with platform specific session display implementations such as iOS's `NowPlayingInfoCenter` and Android's `MediaSession`.

## Features
- Play audio through HTTP streaming.
- Integrate with `NowPlayingInfoCenter` or `MediaSession`.
- Separated concerns, pick and choose what you need.

## Usage
This package provides 3 services, these are:
1. `AudioPlayer`, used to actually play audio using the native implementation.
2. `MediaInfoManager`, used to set and update the session details using `NowPlayingInfoCenter` or `MediaSession`.
3. `AudioPlayerController`, the service which combines the `AudioPlayer` and the `MediaInfoManager` together to provide an all-in-one api.

To add these services to the dependency container, call `UseMauiAudioPlayer` in your `MauiAppBuilderExtensions.cs` file:

```c#
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        .UseMauiAudioPlayer()
        .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });
        
    return builder.Build();
}
```