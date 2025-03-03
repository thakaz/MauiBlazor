using MauiBlazor.Shared.Services;
using Microsoft.FluentUI.AspNetCore.Components;


namespace MauiBlazor.Services;

public class 通知Service : I通知Service
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
