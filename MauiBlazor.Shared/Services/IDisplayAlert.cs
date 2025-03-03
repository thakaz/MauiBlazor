namespace MauiBlazor.Shared.Services;

public interface IDisplayAlert
{
    Task ShowAlertAsync(string title, string message, string cancel);
    Task<bool> ShowAlertAsync(string title, string message, string accept, string cancel);
}
