using ClosedXML.Excel;
using MauiBlazor.Shared.Utils;

namespace MauiBlazor.Shared.Services;



public class 打刻Service
{
    I社員Repository _社員Repository;
    I社員打刻Repository _社員打刻Repository;
    I通知Service _通知Service;
    I音声Service _音声Service;
    天気Service _天気Service;
    IFileUtils _fileUtils;

    public 打刻Service(
        I社員Repository 社員Repository,
        I社員打刻Repository 社員打刻Repository,
        I通知Service 通知Service,
        I音声Service 音声Service,
        天気Service 天気Service,
        IFileUtils fileUtils)
    {
        _社員Repository = 社員Repository;
        _社員打刻Repository = 社員打刻Repository;
        _通知Service = 通知Service;
        _音声Service = 音声Service;
        _天気Service = 天気Service;
        _fileUtils = fileUtils;
    }

    public async Task 打刻byIDm(string idm)
    {
        await 打刻ByIDm(idm, DateTime.UtcNow);
    }

    //idmから社員番号を取得し、打刻処理を行う
    public async Task 打刻ByIDm(string idm, DateTime dateTime)
    {
        //社員番号を取得
        var 社員 = await _社員Repository.GetByカードUIDAsync(idm);
        //打刻処理
        if (社員 != null)
        {
            await 打刻(社員.社員番号, DateTime.UtcNow);
        }
        else
        {
            throw new Exception("社員が見つかりませんでした");
        }
    }

    public async Task 打刻(string 社員番号)
    {
        await 打刻(社員番号, DateTime.Now);
    }

    //idmから社員番号を取得し、打刻処理を行う
    public async Task 打刻(string 社員番号, DateTime dateTime)
    {
        //社員打刻情報を作成
        var 打刻データ = new 社員打刻
        {
            社員番号 = 社員番号,
            打刻時間 = dateTime,
            打刻日 = 出退勤判定Service.打刻日判定(dateTime),
            備考 = ""
        };

        var 周辺打刻 = (await _社員打刻Repository.GetBy社員番号Async(社員番号));

        var 打刻種別 = 出退勤判定Service.判定(打刻データ, 周辺打刻);
        打刻データ.備考 = 打刻種別.ToString();

        await _社員打刻Repository.AddAsync(打刻データ);

        // 通知を表示
        _通知Service.ShowToast(ToastIntent.Success, "打刻しました" + $"{社員番号} {dateTime}");

        // 社員情報を取得
        var 社員 = await _社員Repository.GetBy社員番号Async(社員番号);
        if (社員 != null)
        {
            // 音声タイプを取得（デフォルトはシステム音）
            var 選択音声タイプ = 音声タイプ.システム音;
            if (社員.社員設定 != null)
            {
                選択音声タイプ = (音声タイプ)社員.社員設定.効果音タイプ;
            }

            // 傘が必要かどうかを確認
            var 傘が必要 = await _天気Service.傘が必要か(DateOnly.FromDateTime(dateTime));


            //呼び方の優先度
            var 呼び方 = 社員.ニックネーム ?? 社員.フリガナ性 ?? 社員.名前性;

            // セリフを生成
            var セリフ = _音声Service.打刻セリフ生成(呼び方, 打刻種別.ToString(), 傘が必要);



            // 音声を再生
            await _音声Service.音声再生Async(選択音声タイプ, セリフ);
        }
    }
   
    
    public async Task Excelの出力(string 社員番号, DateOnly 開始日, DateOnly 終了日)
    {
        var 社員打刻s = (await _社員打刻Repository.GetBy社員番号Async(社員番号)).Where(x => x.打刻日 >= 開始日 && x.打刻日 <= 終了日).ToList();
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("勤務報告書");

            // ヘッダーの設定
            worksheet.Cell(1, 1).Value = "日付";
            worksheet.Cell(1, 2).Value = "出勤時間";
            worksheet.Cell(1, 3).Value = "退勤時間";

            int row = 2;
            for (var date = 開始日; date <= 終了日; date = date.AddDays(1))
            {
                var (出勤時間, 退勤時間) = 出退勤判定Service.出退勤判定(社員打刻s, date);

                worksheet.Cell(row, 1).Value = date.ToString("yyyy/MM/dd");
                worksheet.Cell(row, 2).Value = 出勤時間?.ToString("HH:mm:ss") ?? "なし";
                worksheet.Cell(row, 3).Value = 退勤時間?.ToString("HH:mm:ss") ?? "なし";

                row++;
            }

            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "勤務報告書.xlsx");
            workbook.SaveAs(filePath);

            // ファイルをダウンロード
            await _fileUtils.SaveFileAsync(filePath, new CancellationToken(), "勤務報告書.xlsx");
        }
    }



}
