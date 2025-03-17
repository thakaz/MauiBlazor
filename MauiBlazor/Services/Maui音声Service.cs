using MauiBlazor.Shared.Services;
using Plugin.Maui.Audio;
using System.Media;

namespace MauiBlazor.Services;

/// <summary>
/// MAUIプラットフォーム用の音声サービス実装
/// </summary>
public class Maui音声Service : I音声Service
{
    private readonly VoiceBox音声Service _voiceBoxService;
    private readonly 天気Service _天気Service;
    private readonly string _tempAudioPath;
    private IAudioPlayer _audioPlayer;
    private IAudioManager _audioManager;

    public Maui音声Service(VoiceBox音声Service voiceBoxService, 天気Service 天気Service, IAudioManager audioManager)
    {
        _voiceBoxService = voiceBoxService;
        _天気Service = 天気Service;
        _tempAudioPath = System.IO.Path.Combine(FileSystem.CacheDirectory, "temp_audio.wav");
        _audioManager = audioManager;
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

            if (音声データ is null)
            {
                // 音声データが取得できなかった場合はシステム音を再生
                await システム音再生();
                return;
            }
            await 音声データ再生(音声データ);

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
            // MAUIのシステム音を再生
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                // ビープ音を鳴らす
#if ANDROID
                Android.Media.ToneGenerator toneGenerator = new Android.Media.ToneGenerator(Android.Media.Stream.Notification, 100);
                toneGenerator.StartTone(Android.Media.ToneGenerator.ToneBeep, 500);
                await Task.Delay(500);
                toneGenerator.Release();
#elif IOS || MACCATALYST
                AudioToolbox.SystemSound.Vibrate.PlaySystemSound();
#elif WINDOWS
                // Windowsでのシステム音再生
                SystemSounds.Asterisk.Play();
#endif
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"システム音再生エラー: {ex.Message}");
        }
    }

    public async Task 音声データ再生(byte[] audioData)
    {
        try
        {
            // 一時ファイルに音声データを保存
            File.WriteAllBytes(_tempAudioPath, audioData);

            var stream = await FileSystem.OpenAppPackageFileAsync(_tempAudioPath);
            _audioPlayer = _audioManager.CreatePlayer(stream);

            // 再生終了後にストリームをクローズ
            _audioPlayer.PlaybackEnded += (sender, e) =>
            {
                stream.Dispose();
            };

            _audioPlayer.Play();



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
        return VoiceBox音声Service.打刻セリフ生成(社員名, 打刻種別, 傘が必要);
    }
}
