using CommunityToolkit.Maui.Storage;
using MauiBlazor.Shared.Services;
using MauiBlazor.Shared.Utils;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazor.Utils;

public class MAUIFileUtils : IFileUtils
{
    private I通知Service _通知Service { get; set; }

    public MAUIFileUtils(I通知Service 通知Service)
    {
        _通知Service = 通知Service;
    }

    public async Task<string> SaveFileAsync(string filePath, CancellationToken cancellationToken, string defaultFileName = "test.txt")
    {

        try
        {
            byte[] fileData = await File.ReadAllBytesAsync(filePath, cancellationToken);
            using var stream = new MemoryStream(fileData);


            //        using var stream = new MemoryStream(fileStream);
            var saverProgress = new Progress<double>();
            var fileSaverResult = await FileSaver.Default.SaveAsync(defaultFileName, stream, saverProgress, cancellationToken);

            if (fileSaverResult.IsSuccessful)
            {
                _通知Service.ShowToast(ToastIntent.Success, "保存しました。");

            }
            else
            {
                _通知Service.ShowToast(ToastIntent.Error, $"ファイルの保存に失敗しました。{fileSaverResult.Exception.Message}");
            }
            return fileSaverResult.FilePath;

        }
        catch (Exception ex)
        {
            _通知Service.ShowToast(ToastIntent.Error, $"ファイルの保存に失敗しました。{ex.Message}");
            throw;
        }
    }
}
