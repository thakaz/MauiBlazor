using MauiBlazor.Shared.Services;
using MauiBlazor.Shared.Utils;
using Microsoft.FluentUI.AspNetCore.Components;

namespace MauiBlazor.Web.Utils;

public class WebFileUtils : IFileUtils
{
    public Task<string> SaveFileAsync(string filePath, CancellationToken cancellationToken, string defaultFileName)
    {
        throw new NotImplementedException();
    }
}