using MauiBlazor.Shared.Data.Repositories;
using MauiBlazor.Shared.Data;
using MauiBlazor.Shared.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;

using MauiBlazor.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddFluentUIComponents();
builder.Services.AddDbContextFactory<出退勤DbContext>(options =>
{
    string dbPath = Path.Combine("C:\\src", "出退勤.db");  
    // string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "出退勤.db");
    options.UseSqlite($"Data Source={dbPath}");
});

// 各種Serviceの登録
builder.Services.AddScoped<打刻Service>();
builder.Services.AddScoped<社員Service>();
builder.Services.AddScoped<Iカード読み取りService, Webカード読み取りService>();

// 各種Repositoryの登録
builder.Services.AddScoped<I社員Repository, 社員Repository>();
builder.Services.AddScoped<I社員打刻Repository, 社員打刻Repository>();
builder.Services.AddScoped<I社員カードRepository, 社員カードRepository>();

// 通知用
builder.Services.AddSingleton<BlazorToastService>();
builder.Services.AddSingleton<I通知Service, Web通知Service>();
builder.Services.AddScoped<IDisplayAlert, WebDisplayAlert>();

//フォームファクタ
builder.Services.AddSingleton<IFormFactor, FormFactor>();


await builder.Build().RunAsync();
