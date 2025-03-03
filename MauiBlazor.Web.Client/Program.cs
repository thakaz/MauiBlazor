using MauiBlazor.Shared.Data.Repositories;
using MauiBlazor.Shared.Data;
using MauiBlazor.Shared.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;

using MauiBlazor.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddFluentUIComponents();
builder.Services.AddDbContextFactory<�o�ދ�DbContext>(options =>
{
    string dbPath = Path.Combine("C:\\src", "�o�ދ�.db");  
    // string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "�o�ދ�.db");
    options.UseSqlite($"Data Source={dbPath}");
});

// �e��Service�̓o�^
builder.Services.AddScoped<�ō�Service>();
builder.Services.AddScoped<�Ј�Service>();
builder.Services.AddScoped<I�J�[�h�ǂݎ��Service, Web�J�[�h�ǂݎ��Service>();

// �e��Repository�̓o�^
builder.Services.AddScoped<I�Ј�Repository, �Ј�Repository>();
builder.Services.AddScoped<I�Ј��ō�Repository, �Ј��ō�Repository>();
builder.Services.AddScoped<I�Ј��J�[�hRepository, �Ј��J�[�hRepository>();

// �ʒm�p
builder.Services.AddSingleton<BlazorToastService>();
builder.Services.AddSingleton<I�ʒmService, Web�ʒmService>();
builder.Services.AddScoped<IDisplayAlert, WebDisplayAlert>();

//�t�H�[���t�@�N�^
builder.Services.AddSingleton<IFormFactor, FormFactor>();


await builder.Build().RunAsync();
