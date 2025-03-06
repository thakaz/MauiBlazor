using MauiBlazor.Shared.Data;
using MauiBlazor.Shared.Data.Repositories;
using MauiBlazor.Shared.Services;
using MauiBlazor.Shared.Utils;
using MauiBlazor.Web.Components;
using MauiBlazor.Web.Services;
using MauiBlazor.Web.Utils;
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
    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "出退勤.db");
    // string dbPath = Path.Combine(FileSystem.AppDataDirectory, "出退勤.db");
    options.UseSqlite($"Data Source={dbPath}");
});

//各種Serviceの登録
builder.Services.AddScoped<打刻Service>();
builder.Services.AddScoped<社員Service>();
builder.Services.AddScoped<Iカード読み取りService, Webカード読み取りService>();

//各種Repositoryの登録
builder.Services.AddScoped<I社員Repository, 社員Repository>();
builder.Services.AddScoped<I社員打刻Repository, 社員打刻Repository>();
builder.Services.AddScoped<I社員カードRepository, 社員カードRepository>();


//通知用
builder.Services.AddSingleton<BlazorToastService>();
builder.Services.AddSingleton<I通知Service, Web通知Service>();
builder.Services.AddScoped<IDisplayAlert, WebDisplayAlert>();
builder.Services.AddScoped<IFileUtils, WebFileUtils>();


builder.Services.AddSingleton<天気Service>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(MauiBlazor.Shared.Components._Imports).Assembly);

app.Run();

