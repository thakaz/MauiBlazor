using MauiBlazor.Shared.Services;
using Microsoft.FluentUI.AspNetCore.Components;


namespace MauiBlazor.Web.Client.Services;

public class Web通知Service : I通知Service
{
    private readonly BlazorToastService _blazorToastService;


    public Web通知Service(BlazorToastService blazorToastService)
    {
        _blazorToastService = blazorToastService;
    }

    public void ShowToast(ToastIntent intent, string message)
    {
        _blazorToastService.ShowToast(intent, message);
    }

}
