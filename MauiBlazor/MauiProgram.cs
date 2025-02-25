using CommunityToolkit.Maui;
using MauiBlazor.Data;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using Plugin.Maui.Audio;

namespace MauiBlazor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			})
			.UseMauiCommunityToolkit();

		builder.Services.AddMauiBlazorWebView();
        builder.Services.AddFluentUIComponents();
		builder.Services.AddSingleton(AudioManager.Current);

		builder.Services.AddSingleton<出退勤Database>(s => new 出退勤Database(Constants.DatabasePath));



#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
