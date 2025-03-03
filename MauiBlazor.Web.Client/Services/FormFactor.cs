using MauiBlazor.Shared.Services;

namespace MauiBlazor.Web.Client.Services;

class FormFactor : IFormFactor
{
    public string GetFormFactor()
    {
        return "WebAssembly";
    }

    public string GetPlatform()
    {
        return Environment.OSVersion.ToString();
    }
}
