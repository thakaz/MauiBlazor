namespace MauiBlazor.Shared.Services;

public interface Iカード読み取りService
{
    public 読み取り時の処理Code 読み取り時の処理 { get; set; }
    public int? 社員id { get; set; }



    //カードリーダーが稼働中か
    public bool IsMonitoring { get; }

    public bool LoadCardReader();

    bool StartMonitoring();
}

public enum 読み取り時の処理Code
{
    打刻,
    社員マスタ登録
}