using MauiBlazor.Shared.Services;

namespace MauiBlazor.Web.Services;

class WebDisplayAlert : IDisplayAlert
{
    public async Task ShowAlertAsync(string title, string message, string cancel)
    {

    }

    public Task<bool> ShowAlertAsync(string title, string message, string accept, string cancel)
    {
        return Task.FromResult(true);
    }
}

