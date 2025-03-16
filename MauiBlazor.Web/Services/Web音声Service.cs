using MauiBlazor.Shared.Services;
using Microsoft.JSInterop;

namespace MauiBlazor.Web.Services;

/// <summary>
/// Web版アプリケーション用の音声サービス実装
/// </summary>
public class Web音声Service : I音声Service
{
    private readonly VoiceBox音声Service _voiceBoxService;
    private readonly 天気Service _天気Service;
    private readonly IJSRuntime _jsRuntime;

    public Web音声Service(VoiceBox音声Service voiceBoxService, 天気Service 天気Service, IJSRuntime jsRuntime)
    {
        _voiceBoxService = voiceBoxService;
        _天気Service = 天気Service;
        _jsRuntime = jsRuntime;
    }

    /// <summary>
    /// 指定された音声タイプとテキストで音声を再生します
    /// </summary>
    public async Task 音声再生Async(音声タイプ 音声タイプ, string テキスト)
    {
        if (音声タイプ == 音声タイプ.システム音)
        {
            await システム音再生();
            return;
        }

        try
        {
            // VoiceBoxサービスを使用して音声を生成
            var 音声データ = await _voiceBoxService.音声ファイル作成Async(音声タイプ, テキスト);
            音声データ再生(音声データ);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"音声再生エラー: {ex.Message}");
            // エラー時はシステム音を再生
            await システム音再生();
        }
    }

    /// <summary>
    /// システム音を再生します
    /// </summary>
    private async Task システム音再生()
    {
        try
        {
            // Web版のシステム音を再生（JavaScriptを使用）
            await _jsRuntime.InvokeVoidAsync("playBeep");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"システム音再生エラー: {ex.Message}");
        }
    }

    /// <summary>
    /// 音声データを再生します
    /// </summary>
    public async Task 音声データ再生(byte[] audioData)
    {
        try
        {
            // Web Audio APIを使用して音声を再生（JavaScriptを使用）
            var base64Audio = Convert.ToBase64String(audioData);
            await _jsRuntime.InvokeVoidAsync("playAudio", $"data:audio/wav;base64,{base64Audio}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"音声データ再生エラー: {ex.Message}");
        }
    }

    /// <summary>
    /// 天気情報に基づいて打刻時のセリフを生成します
    /// </summary>
    public string 打刻セリフ生成(string 社員名, string 打刻種別, bool 傘が必要)
    {
        return _voiceBoxService.打刻セリフ生成(社員名, 打刻種別, 傘が必要);
    }
}
