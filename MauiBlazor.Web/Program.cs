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
builder.Services.AddDbContextFactory<�o�ދ�DbContext>(options =>
{
    //��������DB�p�X�𖾎��I��
    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "�o�ދ�.db");
    // string dbPath = Path.Combine(FileSystem.AppDataDirectory, "�o�ދ�.db");
    options.UseSqlite($"Data Source={dbPath}");
});

//�e��Service�̓o�^
builder.Services.AddScoped<�ō�Service>();
builder.Services.AddScoped<�Ј�Service>();
builder.Services.AddScoped<I�J�[�h�ǂݎ��Service, Web�J�[�h�ǂݎ��Service>();

//�e��Repository�̓o�^
builder.Services.AddScoped<I�Ј�Repository, �Ј�Repository>();
builder.Services.AddScoped<I�Ј��ō�Repository, �Ј��ō�Repository>();
builder.Services.AddScoped<I�Ј��J�[�hRepository, �Ј��J�[�hRepository>();


//�ʒm�p
builder.Services.AddSingleton<BlazorToastService>();
builder.Services.AddSingleton<I�ʒmService, Web�ʒmService>();
builder.Services.AddScoped<IDisplayAlert, WebDisplayAlert>();
builder.Services.AddScoped<IFileUtils, WebFileUtils>();


builder.Services.AddSingleton<�V�CService>();


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

