using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazor.Shared.Services;

namespace MauiBlazor.Services;

class MauiDisplayAlert : IDisplayAlert
{
    public async Task ShowAlertAsync(string title, string message, string cancel)
    {
        await Application.Current.MainPage.DisplayAlert(title, message, cancel);
    }

    public Task<bool> ShowAlertAsync(string title, string message, string accept, string cancel)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, accept,cancel);
    }
}

