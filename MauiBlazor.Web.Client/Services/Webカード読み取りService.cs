using MauiBlazor.Shared.Services;

namespace MauiBlazor.Web.Client.Services;

public class Webカード読み取りService : Iカード読み取りService
{

    public 読み取り時の処理Code 読み取り時の処理 { get; set; } = 読み取り時の処理Code.打刻;
    public int? 社員id { get; set; } = null;


    public void StartMonitoring()
    {
        //Web側では何もしない。
    }

}
