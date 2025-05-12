using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Maui.Audio.Player.AudioPlayerController;
using Microsoft.Extensions.Configuration;

namespace Maui.Audio.Player.Sample;

public class MainPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private readonly IAudioPlayerController _audioPlayerController;
    private readonly List<string> _audioUrls;
    private int _index = 0;
    
    public ICommand PlayPauseCommand { get; }
    
    public ICommand SkipToPreviousCommand { get; }
    public ICommand SkipToNextCommand { get; }
    
    public MainPageViewModel(IAudioPlayerController audioPlayerController, IConfiguration configuration)
    {
        _audioPlayerController = audioPlayerController;
        
        _audioUrls = configuration
            .GetSection("AudioUrls")
            .GetChildren()
            .Select(item => item.Value!)
            .ToList();

        PlayPauseCommand = new Command(PlayPause);

        SkipToPreviousCommand = new Command(SkipToPrevious);
        SkipToNextCommand = new Command(SkipToNext);
    }

    private void PlayPause()
    {
        var playerInfo = _audioPlayerController.PlayerInfo;

        if (!playerInfo.IsPlaying)
        {
            PlayNewSong();
            _audioPlayerController.Play();

            return;
        }
        
        _audioPlayerController.Pause();
    }
    
    private void SkipToPrevious()
    {
        _index =- 1;
        
        if (_index < 0)
            _index = _audioUrls.Count - 1;
        
        PlayNewSong();
    }
    
    private void SkipToNext()
    {
        _index =+ 1;
        
        if (_index >= _audioUrls.Count)
            _index = 0;
        
        PlayNewSong();
    }

    private void PlayNewSong()
    {
        var url = _audioUrls[_index];
        var mediaInfo = new MediaInfo($"Song {_index}", $"Artist {_index}", 100);
        
        _audioPlayerController.Start(url, mediaInfo);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) 
            return false;
        
        field = value;
        OnPropertyChanged(propertyName);
        
        return true;
    }
}