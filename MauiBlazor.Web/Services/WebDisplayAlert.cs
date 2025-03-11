using MauiBlazor.Shared.Services;
using Microsoft.JSInterop;

namespace MauiBlazor.Web.Services;

class WebDisplayAlert : IDisplayAlert
{

    private readonly IJSRuntime _jsRuntime;


    public WebDisplayAlert(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task ShowAlertAsync(string title, string message, string cancel)
    {
        await _jsRuntime.InvokeVoidAsync("alert", message);
    }

    public async Task<bool> ShowAlertAsync(string title, string message, string accept, string cancel)
    {
        var result = await _jsRuntime.InvokeAsync<bool>("confirm", $"{title}\n\n{message}");
        return result;
    }
}

