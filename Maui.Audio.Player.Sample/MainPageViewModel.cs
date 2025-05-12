using System.ComponentModel;
using System.Runtime.CompilerServices;
using Maui.Audio.Player.AudioPlayerController;
using Microsoft.Extensions.Configuration;

namespace Maui.Audio.Player.Sample;

public class MainPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private readonly IAudioPlayerController _audioPlayerController;
    private readonly List<string> _audioUrls;
    
    public MainPageViewModel(IAudioPlayerController audioPlayerController, IConfiguration configuration)
    {
        _audioPlayerController = audioPlayerController;
        
        _audioUrls = configuration
            .GetSection("AudioUrls")
            .GetChildren()
            .Select(item => item.Value!)
            .ToList();
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