# Maui.Audio.Player

A package which allows you to play audio in your MAUI application. Either use it as a standalone solution or integrate with platform-specific session display implementations such as iOS's `NowPlayingInfoCenter` and Android's `MediaSession`.

> [!NOTE]  
> This package is still in development. Currently, this package is stable and ready to use, but it lacks some nice-to-have features like seeking the audio through the session display notification. If you need such features, you're welcome to submit an issue or PR and I'll take a look into it. 

## Features
- Play audio through HTTP streaming.
- Integrate with `NowPlayingInfoCenter` or `MediaSession`.
- Separated concerns, pick and choose what you need.

## Installation
`Maui.Audio.Player` can be found on [NuGet](https://www.nuget.org/packages/Euphiny.Maui.Audio.Player/). Install it by using `dotnet add package Euphiny.Maui.Audio.Player`. 

[![NuGet](https://img.shields.io/nuget/v/Euphiny.Maui.Audio.Player.svg?label=NuGet)](https://www.nuget.org/packages/Euphiny.Maui.Audio.Player/)

## Usage
This package provides 3 services, these are:
1. `AudioPlayer`, used to actually play audio using the native implementation.
2. `MediaInfoManager`, used to set and update the session details using `NowPlayingInfoCenter` or `MediaSession`.
3. `AudioPlayerController`, the service which combines the `AudioPlayer` and the `MediaInfoManager` together to provide an all-in-one api.

To add these services to the dependency container, call `UseMauiAudioPlayer` in your `MauiAppBuilderExtensions.cs` file. It is advised to create an AudioPlayerOptions object and use it as an argument to initialize the audio player. Currently only the IconResource is requested on Android, which is used by the media notification as app icon.

```c#
public static MauiApp CreateMauiApp()
{
    var audioPlayerOptions = new AudioPlayerOptions();
    
    #if ANDROID
    audioPlayerOptions.IconResource = Resource.Drawable.Icon;
    #endif
    
    var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        .UseMauiAudioPlayer(audioPlayerOptions)
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
    var url = "https://song-url.com";
    var imageUrl = "https://image-url.com";
    var duration = TimeSpan.FromSeconds(150);
    var artists = new List<string>() 
    {
        "Artist 1",
        "Artist 2"
    };
    
    var mediaInfo = new MediaInfo("Name of the song", string.Join(", ", artists), duration, imageUrl);
        
    _audioPlayerController.Start(url, mediaInfo);
}
```

The `AudioPlayerController` exposes other methods besides the `Start` method:
- `Play()`, to tell the player to start playing audio, is called internally when starting the player.
- `Pause()`, to pause the player
- `Seek()`, to move the current position of the player backwards or forwards.

Furthermore, the `PlayerInfo` property holds the current state of the player, information such as duration, progress and whether the player is playing any audio can be found here.

When the player is done playing the audio stream, the `PlaybackEnded` event is fired.