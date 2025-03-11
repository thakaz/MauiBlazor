using MauiBlazor.Shared.Utils;
using Microsoft.JSInterop;

namespace MauiBlazor.Web.Utils;

public class WebFileUtils : IFileUtils
{
    IJSRuntime _jsRuntime;

    //JSをDI
    public WebFileUtils(IJSRuntime jSRuntime)
    {
        _jsRuntime = jSRuntime;
    }


    public async Task<string> SaveFileAsync(string filePath, CancellationToken cancellationToken, string defaultFileName)
    {

        //GetFileStream
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        using var streamRef = new DotNetStreamReference(fileStream);
        return await _jsRuntime.InvokeAsync<string>("downloadFileFromStream", defaultFileName, streamRef);


    }
}