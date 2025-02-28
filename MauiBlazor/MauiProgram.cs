using CommunityToolkit.Maui;
using MauiBlazor.Data;
using MauiBlazor.Data.Repositories;
using MauiBlazor.Services;
using Microsoft.EntityFrameworkCore;
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

        builder.Services.AddDbContextFactory<出退勤DbContext>(options =>
        {
            //いったんDBパスを明示的に
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "出退勤.db");
           // string dbPath = Path.Combine(FileSystem.AppDataDirectory, "出退勤.db");
            options.UseSqlite($"Data Source={dbPath}");

            //options.UseSqlite(Constants.DatabasePath);

        });

        //各種Serviceの登録
        builder.Services.AddSingleton<打刻Service>();
        builder.Services.AddSingleton<社員Service>();
        builder.Services.AddSingleton<カード読み取りService>();

        //各種Repositoryの登録
        builder.Services.AddScoped<I社員Repository, 社員Repository>();
        builder.Services.AddScoped<I社員打刻Repository, 社員打刻Repository>();
        builder.Services.AddScoped<I社員カードRepository, 社員カードRepository>();



#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
