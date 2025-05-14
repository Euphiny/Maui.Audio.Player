using Maui.Audio.Player.AudioPlayerController;
using Maui.Audio.Player.MediaInfoManager;
using Microsoft.Extensions.Configuration;

namespace Maui.Audio.Player.Sample;

public partial class MainPage : ContentPage
{
	public MainPage(
		IAudioPlayerController audioPlayerController,
		IMediaInfoManager mediaInfoManager, 
		IConfiguration configuration)
	{
		BindingContext = new MainPageViewModel(audioPlayerController, mediaInfoManager, configuration);
		
		InitializeComponent();
	}
}

