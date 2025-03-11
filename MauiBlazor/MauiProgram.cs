using CommunityToolkit.Maui;
using MauiBlazor.Services;
using MauiBlazor.Shared;
using MauiBlazor.Shared.Data;
using MauiBlazor.Shared.Data.Repositories;
using MauiBlazor.Shared.Helper.Auth;
using MauiBlazor.Shared.Services;
using MauiBlazor.Shared.Utils;
using MauiBlazor.Utils;
using Microsoft.AspNetCore.Components.Authorization;
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
  .UseMauiCommunityToolkit(options =>
  {
      options.SetShouldEnableSnackbarOnWindows(true);
  });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddFluentUIComponents();
        builder.Services.AddSingleton(AudioManager.Current);

        builder.Services.AddDbContextFactory<出退勤DbContext>(options =>
        {
            // string dbPath = Path.Combine(FileSystem.AppDataDirectory, "出退勤.db");
            //options.UseSqlite($"Data Source={dbPath}");

            options.UseNpgsql(Constants.ConnectionString);

            //options.UseSqlite(Constants.DatabasePath);

        });



        //各種Serviceの登録
        builder.Services.AddSingleton<打刻Service>();
        builder.Services.AddSingleton<社員Service>();
        builder.Services.AddSingleton<Iカード読み取りService, カード読み取りService>();

        //各種Repositoryの登録
        builder.Services.AddScoped<I社員Repository, 社員Repository>();
        builder.Services.AddScoped<I社員打刻Repository, 社員打刻Repository>();
        builder.Services.AddScoped<I社員カードRepository, 社員カードRepository>();
        builder.Services.AddScoped<I組織Repository, 組織Repository>();


        //通知用
        builder.Services.AddSingleton<BlazorToastService>();
        builder.Services.AddSingleton<I通知Service, 通知Service>();
        builder.Services.AddSingleton<IDisplayAlert, MauiDisplayAlert>();
        builder.Services.AddSingleton<IFileUtils, MAUIFileUtils>();

        builder.Services.AddScoped<MyAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<MyAuthenticationStateProvider>());

        builder.Services.AddAuthorizationCore();

        builder.Services.AddSingleton<天気Service>();


#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
