using Maui.Audio.Player.AudioPlayerController;
using Microsoft.Extensions.Configuration;

namespace Maui.Audio.Player.Sample;

public partial class MainPage : ContentPage
{
	public MainPage(IAudioPlayerController audioPlayerController, IConfiguration configuration)
	{
		BindingContext = new MainPageViewModel(audioPlayerController, configuration);
		
		InitializeComponent();
	}
}

