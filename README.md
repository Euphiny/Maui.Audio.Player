# Maui.Audio.Player

A package which allows you to play audio in your MAUI application. Either use it as standalone solution or integrate with platform specific session display implementations such as iOS's `NowPlayingInfoCenter` and Android's `MediaSession`.

> [!WARNING]  
> The integration with `MediaSession` on Android is not yet fully implemented.

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

It is than possible to take `IAudioPlayerController` as constructor argument and use it to start playing audio:

```c#
public void PlayAudio() 
{
    var url = "https://song-url.com"
    var durationInSeconds = 178.2;
    var artists = new List<string>() 
    {
        "Artist 1",
        "Artist 2"
    }
    
    var mediaInfo = new MediaInfo("Name of the song", string.Join(", ", artists), durationInSeconds);
        
    _audioPlayerController.Start(url, mediaInfo);
}
```

The `AudioPlayerController` exposes other methods besides the `Start` method:
- `Play()`, to tell the player to start playing audio, is called internally when starting the player.
- `Pause()`, to pause the player
- `Seek()`, to move the current position of the player backwards or forwards.

Furthermore, the `PlayerInfo` property holds the current state of the player, information such as duration, progress and whether the player is playing any audio can be found here.

When the player is done playing the audio stream, the `PlaybackEnded` event is fired.