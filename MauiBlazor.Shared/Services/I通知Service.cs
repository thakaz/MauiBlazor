namespace MauiBlazor.Shared.Services;

public interface I通知Service
{
    void ShowToast(ToastIntent intent, string message);
}

/// <summary>
/// Blazor側の通知?
/// </summary>
public class BlazorToastService
{
    public event Action<ToastIntent, string>? OnShowToast;

    public void ShowToast(ToastIntent intent, string message)
    {
        OnShowToast?.Invoke(intent, message);
    }
}
