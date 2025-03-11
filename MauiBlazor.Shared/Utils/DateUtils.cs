namespace MauiBlazor.Shared.Utils;

public static class DateUtils
{

    /// <summary>
    /// DateOnly?型の日付を受け取り、その日付が属する年度を取得するメソッド
    /// </summary>
    /// <param name="date">日付</param>
    /// <returns>年度</returns>
    public static int? GetFiscalYear(DateOnly? date)
    {
        return date.HasValue ? CalculateFiscalYear(date.Value.Month, date.Value.Year) : (int?)null;
    }

    /// <summary>
    /// DateTime?型の日付を受け取り、その日付が属する年度を取得するメソッド
    /// </summary>
    /// <param name="date">日付</param>
    /// <returns>年度</returns>
    public static int? GetFiscalYear(DateTime? date)
    {
        return date.HasValue ? CalculateFiscalYear(date.Value.Month, date.Value.Year) : (int?)null;
    }

    /// <summary>
    /// 月と年を受け取り、その日付が属する年度を計算するプライベートメソッド
    /// </summary>
    /// <param name="month">月</param>
    /// <param name="year">年</param>
    /// <returns>年度</returns>
    private static int CalculateFiscalYear(int month, int year)
    {
        // 4月以降ならその年、1月～3月なら前年を年度とする
        return month >= 4 ? year : year - 1;
    }

    /// <summary>
    /// YYYYMMDD形式の文字列をDateOnly?型に変換。
    /// </summary>
    /// <param name="yyyyMMdd">YYYYMMDD形式の文字列</param>
    /// <returns>変換されたDateOnly?型の値。形式が正しくない場合はnullを返す。</returns>
    public static DateOnly? ConvertToDateOnly(string yyyyMMdd)
    {
        if (string.IsNullOrWhiteSpace(yyyyMMdd) || yyyyMMdd.Length != 8)
        {
            return null;
        }

        if (int.TryParse(yyyyMMdd.Substring(0, 4), out int year) &&
            int.TryParse(yyyyMMdd.Substring(4, 2), out int month) &&
            int.TryParse(yyyyMMdd.Substring(6, 2), out int day))
        {
            try
            {
                return new DateOnly(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                // 無効な日付の場合
                return null;
            }
        }

        return null;
    }

    //月度の範囲を返す
    public static (DateOnly firstDay, DateOnly lastDay) GetMonthRange(DateOnly date)
    {
        var firstDay = new DateOnly(date.Year, date.Month - 1, 16);
        var lastDay = new DateOnly(date.Year, date.Month, 15);
        return (firstDay, lastDay);
    }
}
