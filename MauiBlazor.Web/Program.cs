using MauiBlazor.Shared;
using MauiBlazor.Shared.Data;
using MauiBlazor.Shared.Data.Repositories;
using MauiBlazor.Shared.Helper.Auth;
using MauiBlazor.Shared.Services;
using MauiBlazor.Shared.Utils;
using MauiBlazor.Web.Components;
using MauiBlazor.Web.Services;
using MauiBlazor.Web.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddFluentUIComponents();
builder.Services.AddDbContextFactory<出退勤DbContext>(options =>
{
    //いったんDBパスを明示的に
    //string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "出退勤.db");
    // string dbPath = Path.Combine(FileSystem.AppDataDirectory, "出退勤.db");
    // options.UseSqlite($"Data Source={dbPath}");

    options.UseNpgsql(Constants.ConnectionString);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

//各種Serviceの登録
builder.Services.AddScoped<打刻Service>();
builder.Services.AddScoped<社員Service>();
builder.Services.AddScoped<Iカード読み取りService, Webカード読み取りService>();

//各種Repositoryの登録
builder.Services.AddScoped<I社員Repository, 社員Repository>();
builder.Services.AddScoped<I社員打刻Repository, 社員打刻Repository>();
builder.Services.AddScoped<I社員カードRepository, 社員カードRepository>();
builder.Services.AddScoped<I組織Repository, 組織Repository>();


//通知用
builder.Services.AddSingleton<BlazorToastService>();
builder.Services.AddSingleton<I通知Service, Web通知Service>();
builder.Services.AddScoped<IDisplayAlert, WebDisplayAlert>();
builder.Services.AddScoped<IFileUtils, WebFileUtils>();


builder.Services.AddScoped<MyAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<MyAuthenticationStateProvider>());

builder.Services.AddSingleton<天気Service>();

// 音声サービスの登録
builder.Services.AddHttpClient();
builder.Services.AddSingleton<VoiceBox音声Service>();
builder.Services.AddScoped<I音声Service, Web音声Service>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseStaticFiles(); // JavaScriptファイルを追加
app.UseAntiforgery();

app.UseAuthentication(); // 認証ミドルウェアを追加
app.UseAuthorization(); // 認可ミドルウェアを追加

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(MauiBlazor.Shared.Components._Imports).Assembly);

app.Run();
