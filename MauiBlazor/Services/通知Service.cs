using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using Microsoft.FluentUI.AspNetCore.Components;
using Npgsql.Replication.PgOutput.Messages;

namespace MauiBlazor.Services;

public interface I通知Service
{
    void ShowToast(ToastIntent intent, string message);
}

public  class 通知Service : I通知Service
{

    private readonly BlazorToastService _blazorToastService;
    private readonly IDispatcher _dispatcher;

    public 通知Service(BlazorToastService toastService, IDispatcher dispatcher)
    {
        _blazorToastService = toastService;
        _dispatcher = dispatcher;
    }
    public void ShowToast(ToastIntent intent, string message)
    {
        _dispatcher.Dispatch(() =>
        _blazorToastService.ShowToast(intent, message));
    }

}

/// <summary>
/// Blazor側の通知?
/// </summary>
public class BlazorToastService
{
    public event Action<ToastIntent,string>? OnShowToast;

    public void ShowToast(ToastIntent intent,string message)
    {
        OnShowToast?.Invoke(intent,message);
    }
}
