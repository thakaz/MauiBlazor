namespace MauiBlazor.Shared.Services;

/// <summary>
/// 音声タイプの列挙型
/// </summary>
public enum 音声タイプ
{
    システム音 = 0,
    ずんだもん = 1,
    めたん = 2
}

/// <summary>
/// 音声サービスのインターフェース
/// </summary>
public interface I音声Service
{
    /// <summary>
    /// 指定された音声タイプとテキストで音声を再生します
    /// </summary>
    /// <param name="音声タイプ">音声タイプ（システム音、ずんだもん、めたん）</param>
    /// <param name="テキスト">読み上げるテキスト</param>
    /// <returns>非同期タスク</returns>
    Task 音声再生Async(音声タイプ 音声タイプ, string テキスト);

    /// <summary>
    /// 天気情報に基づいて打刻時のセリフを生成します
    /// </summary>
    /// <param name="社員名">社員の名前</param>
    /// <param name="打刻種別">打刻の種別（出勤、退勤など）</param>
    /// <param name="傘が必要">傘が必要かどうか</param>
    /// <returns>生成されたセリフ</returns>
    string 打刻セリフ生成(string 社員名, string 打刻種別, bool 傘が必要);
}
