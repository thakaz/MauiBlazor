using System.Text;

namespace MauiBlazor.Shared.Services;

/// <summary>
/// VoiceBoxを使用した音声サービスの実装
/// </summary>
public class VoiceBox音声Service
{
    private readonly HttpClient _httpClient;
    private readonly 天気Service _天気Service;
    private readonly string _apiEndpoint = "http://localhost:50021/audio_query";
    private readonly string _synthesisEndpoint = "http://localhost:50021/synthesis";

    // スピーカーID
    private const int ずんだもんID = 3;  // ずんだもん（ノーマル）
    private const int めたんID = 2;      // めたん（ノーマル）

    public VoiceBox音声Service(HttpClient httpClient, 天気Service 天気Service)
    {
        _httpClient = httpClient;
        _天気Service = 天気Service;
    }

    /// <summary>
    /// 指定された音声タイプとテキストで音声を再生します
    /// </summary>
    public async Task<byte[]?> 音声ファイル作成Async(音声タイプ 音声タイプ, string テキスト)
    {
        if (音声タイプ == 音声タイプ.システム音)
        {
            return null;
        }
        try
        {
            // 音声タイプに応じたスピーカーIDを取得
            int speakerId = 音声タイプ == 音声タイプ.ずんだもん ? ずんだもんID : めたんID;

            // 音声クエリを生成
            var queryParams = new Dictionary<string, string>
            {
                { "text", テキスト },
                { "speaker", speakerId.ToString() }
            };
            var queryString = await new FormUrlEncodedContent(queryParams).ReadAsStringAsync();
            var queryUrl = $"{_apiEndpoint}?{queryString}";

            // 音声クエリを取得
            var queryResponse = await _httpClient.PostAsync(queryUrl, null);
            queryResponse.EnsureSuccessStatusCode();
            var queryJson = await queryResponse.Content.ReadAsStringAsync();

            // 音声合成
            var synthesisParams = new Dictionary<string, string>
            {
                { "speaker", speakerId.ToString() }
            };
            var synthesisQueryString = await new FormUrlEncodedContent(synthesisParams).ReadAsStringAsync();
            var synthesisUrl = $"{_synthesisEndpoint}?{synthesisQueryString}";

            var content = new StringContent(queryJson, Encoding.UTF8, "application/json");
            var synthesisResponse = await _httpClient.PostAsync(synthesisUrl, content);
            synthesisResponse.EnsureSuccessStatusCode();

            // 音声データを取得
            return await synthesisResponse.Content.ReadAsByteArrayAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"音声再生エラー: {ex.Message}");
            return null; // Ensure a return value in case of an exception
        }
    }

    /// <summary>
    /// 天気情報に基づいて打刻時のセリフを生成します
    /// </summary>
    public static string 打刻セリフ生成(string 社員名, string 打刻種別, bool 傘が必要)
    {
        var 時間帯 = DateTime.Now.Hour switch
        {
            >= 5 and < 10 => "朝",
            >= 10 and < 15 => "昼",
            >= 15 and < 18 => "夕方",
            _ => "夜"
        };

        var 基本セリフ = 打刻種別 switch
        {
            "出勤" => $"{社員名}さん、おはようございます。{時間帯}の出勤を記録しました。",
            "退勤" => $"{社員名}さん、お疲れ様でした。{時間帯}の退勤を記録しました。",
            _ => $"{社員名}さん、{打刻種別}を記録しました。"
        };

        var 天気セリフ = 傘が必要 switch
        {
            true when 打刻種別 == "出勤" => "今日は雨や雪の予報です。傘を忘れないでくださいね。",
            true when 打刻種別 == "退勤" => "外は雨や雪のようです。お気をつけてお帰りください。",
            false when 打刻種別 == "出勤" => "今日は良い天気になりそうです。良い一日をお過ごしください。",
            false when 打刻種別 == "退勤" => "明日も良い天気になりそうです。ゆっくり休んでくださいね。",
            _ => ""
        };

        return $"{基本セリフ} {天気セリフ}";
    }
}
