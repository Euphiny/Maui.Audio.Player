using Maui.Audio.Player.AudioPlayerController;
using Microsoft.Extensions.Configuration;

namespace Maui.Audio.Player.Sample;

public partial class MainPage : ContentPage
{
	private readonly IAudioPlayerController _audioPlayerController;
	private readonly IConfiguration _configuration;
	
	public MainPage(IAudioPlayerController audioPlayerController, IConfiguration configuration)
	{
		_audioPlayerController = audioPlayerController;
		_configuration = configuration;
		
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		var section = _configuration.GetSection("AudioUrls");
		var enumerable = section.GetChildren();
		var url = enumerable.FirstOrDefault()?.Value;
		var mediaInfo = new MediaInfo("Name of song", "Artist name", 100);

		if (string.IsNullOrEmpty(url))
			throw new NullReferenceException("Make sure to set AudioUrl in your appsettings.json file.");
		
		_audioPlayerController.Start(url, mediaInfo);
	}
}

