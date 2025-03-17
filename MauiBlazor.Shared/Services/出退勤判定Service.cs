using System.Diagnostics;

namespace MauiBlazor.Shared.Services;

public enum 出退勤判定コード
{
    出勤,
    退勤,
    多分出勤,
    多分退勤,
    不明
}

public enum 時間区分
{
    朝,
    昼,
    夜
}

public class 判定ルール
{
    public 時間区分 時間帯 { get; set; }
    public bool 打刻済み { get; set; }
    public 出退勤判定コード 判定 { get; set; }
}

public static class 出退勤判定Service
{
    static int 一日の開始 = 5;

    public static 時間区分 時間判定(DateTime 打刻時間)
    {
        if (打刻時間.Hour >= 5 && 打刻時間.Hour <= 11)
        {
            return 時間区分.朝;
        }
        else if (打刻時間.Hour > 11 && 打刻時間.Hour < 16)
        {
            return 時間区分.昼;
        }
        else
        {
            return 時間区分.夜;
        }

    }

    //出退勤判定のテーブル駆動
    private static readonly List<判定ルール> 判定ルールテーブル = new List<判定ルール>
        {
            new 判定ルール { 時間帯 = 時間区分.朝, 打刻済み = false, 判定 = 出退勤判定コード.出勤 },
            new 判定ルール { 時間帯 = 時間区分.朝, 打刻済み = true, 判定 = 出退勤判定コード.多分出勤 },
            new 判定ルール { 時間帯 = 時間区分.昼, 打刻済み = false, 判定 = 出退勤判定コード.多分出勤 },
            new 判定ルール { 時間帯 = 時間区分.昼, 打刻済み = true, 判定 = 出退勤判定コード.多分退勤 },
            new 判定ルール { 時間帯 = 時間区分.夜, 打刻済み = false, 判定 = 出退勤判定コード.多分退勤 },
            new 判定ルール { 時間帯 = 時間区分.夜, 打刻済み = true, 判定 = 出退勤判定コード.退勤 },
        };


    public static 出退勤判定コード 判定(社員打刻 対象打刻データ, List<社員打刻> 周辺打刻データ)
    {

        //打刻時間から時間帯を取得
        var target時間帯 = 時間判定(対象打刻データ.打刻時間);

        TimeOnly target打刻時間 = new TimeOnly(対象打刻データ.打刻時間.Hour, 対象打刻データ.打刻時間.Minute, 対象打刻データ.打刻時間.Second)
                                    .AddHours(-一日の開始);

        //打刻時間から対象日を取得
        var target日付 = 打刻日判定(対象打刻データ.打刻時間);

        //周辺打刻データから対象日の打刻データを取得
        var target打刻済み = 周辺打刻データ
            .Where(x => x.打刻時間 >= target日付.ToDateTime(new TimeOnly(5, 0, 0))
                           && x.打刻時間 < target日付.ToDateTime(new TimeOnly(5, 0, 0)).AddDays(1))
            .ToList().Count > 0;

        //判定ルールテーブルから対象のルールを取得
        var rule = 判定ルールテーブル
            .Where(x => x.時間帯 == target時間帯 && x.打刻済み == target打刻済み)
            .FirstOrDefault();

        if (rule == null)
            return 出退勤判定コード.不明;

        return rule.判定;

    }


    public static DateOnly 打刻日判定(DateTimeOffset 打刻時間)
    {
        try
        {
            return DateOnly.FromDateTime(打刻時間.AddHours(-一日の開始).Date);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return DateOnly.FromDateTime(DateTime.Now.Date);
        }
    }

    public static (DateTime? 出勤時間, DateTime? 退勤時間) 出退勤判定(List<社員打刻> 打刻データ, DateOnly 日付)
    {
        var 出勤時間 = 打刻データ
            .Where(x => x.打刻時間.Date == 日付.ToDateTime(new TimeOnly()).Date && x.備考 == "出勤")
            .Select(x => (DateTime?)x.打刻時間)
            .FirstOrDefault();

        var 退勤時間 = 打刻データ
            .Where(x => x.打刻時間.Date == 日付.ToDateTime(new TimeOnly()).Date && x.備考 == "退勤")
            .Select(x => (DateTime?)x.打刻時間)
            .FirstOrDefault();

        return (出勤時間, 退勤時間);
    }

}
