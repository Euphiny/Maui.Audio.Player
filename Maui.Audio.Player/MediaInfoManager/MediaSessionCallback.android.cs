using Android.Support.V4.Media.Session;

namespace Maui.Audio.Player.MediaInfoManager;

public class MediaSessionCallback : MediaSessionCompat.Callback
{
    private readonly MediaNotificationManager _notificationManager = new();
    
    public Action? OnPlayCommand { get; set; }
    public Action? OnPauseCommand { get; set; }
    public Action? OnSkipToNextCommand { get; set; }
    public Action? OnSkipToPreviousCommand { get; set; }
    
    public override void OnPlay()
    {
        OnPlayCommand?.Invoke();
    }

    public override void OnPause()
    {
        OnPauseCommand?.Invoke();
    }

    public override void OnSkipToNext()
    {
        OnSkipToNextCommand?.Invoke();
    }

    public override void OnSkipToPrevious()
    {
        OnSkipToPreviousCommand?.Invoke();
    }
}