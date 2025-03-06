using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Storage;
using MauiBlazor.Shared.Services;

namespace MauiBlazor.Shared.Utils;

public static class FileUtils
{

    public static async Task SaveFile(string filePath ,CancellationToken cancellationToken,I通知Service 通知Service,string defaultFileName="test.txt")
    {

        byte[] fileData = await File.ReadAllBytesAsync(filePath, cancellationToken);
        using var stream = new MemoryStream(fileData);


        //        using var stream = new MemoryStream(fileStream);
        var saverProgress = new Progress<double>();
        var fileSaverResult = await FileSaver.Default.SaveAsync(defaultFileName, stream, saverProgress, cancellationToken);

        if (fileSaverResult.IsSuccessful)
        {
            通知Service.ShowToast(ToastIntent.Success, "保存しました。");
        }
        else
        {
            通知Service.ShowToast(ToastIntent.Error, $"ファイルの保存に失敗しました。{fileSaverResult.Exception.Message}");
        }
    }

}
