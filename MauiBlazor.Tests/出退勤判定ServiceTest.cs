using MauiBlazor.Shared.Models;
using MauiBlazor.Shared.Services;

namespace MauiBlazor.Tests;

public class 出退勤判定ServiceTest
{
    // 時間判定メソッドのテスト
    [Theory]
    [InlineData(5, 0, 0, "朝")] // 朝の開始時間
    [InlineData(11, 59, 59, "朝")] // 朝の終了時間
    [InlineData(12, 0, 0, "昼")] // 昼の開始時間
    [InlineData(15, 59, 59, "昼")] // 昼の終了時間
    [InlineData(16, 0, 0, "夜")] // 夜の開始時間
    [InlineData(23, 59, 59, "夜")] // 夜の深夜時間
    [InlineData(0, 0, 0, "夜")] // 夜の深夜時間（翌日）
    [InlineData(4, 59, 59, "夜")] // 夜の終了時間（翌日）
    public void 時間判定_正しい時間帯を返す(int hour, int minute, int second, string expected)
    {
        // Arrange
        var dateTime = new DateTime(2025, 3, 13, hour, minute, second);

        // Act
        var result = 出退勤判定Service.時間判定(dateTime);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected, result.ToString());
    }

    // 打刻日判定メソッドのテスト
    [Theory]
    [InlineData(2025, 3, 13, 4, 0, 0, "2025/03/12")] // 5時前は前日の日付
    [InlineData(2025, 3, 13, 5, 0, 0, "2025/03/13")] // 5時以降は当日の日付
    [InlineData(2025, 3, 13, 23, 59, 59, "2025/03/13")] // 深夜も当日の日付
    public void 打刻日判定_正しい日付を返す(int year, int month, int day, int hour, int minute, int second, string expected)
    {
        // Arrange
        var dateTime = new DateTime(year, month, day, hour, minute, second);

        // Act
        var result = 出退勤判定Service.打刻日判定(dateTime);

        // Assert
        Assert.Equal(expected, result.ToString("yyyy/MM/dd"));
    }

    // 判定メソッドのテスト
    [Fact]
    public void 判定_朝_打刻なし_出勤を返す()
    {
        // Arrange
        var 対象打刻データ = new 社員打刻
        {
            社員番号 = "001",
            打刻時間 = new DateTime(2025, 3, 13, 8, 0, 0), // 朝8時
            打刻日 = new DateOnly(2025, 3, 13)
        };

        var 周辺打刻データ = new List<社員打刻>(); // 打刻なし

        // Act
        var result = 出退勤判定Service.判定(対象打刻データ, 周辺打刻データ);

        // Assert
        Assert.Equal("出勤", result.ToString());
    }

    [Fact]
    public void 判定_朝_打刻あり_多分出勤を返す()
    {
        // Arrange
        var 対象打刻データ = new 社員打刻
        {
            社員番号 = "001",
            打刻時間 = new DateTime(2025, 3, 13, 8, 0, 0), // 朝8時
            打刻日 = new DateOnly(2025, 3, 13)
        };

        var 周辺打刻データ = new List<社員打刻>
        {
            new 社員打刻
            {
                社員番号 = "001",
                打刻時間 = new DateTime(2025, 3, 13, 6, 0, 0), // 朝6時
                打刻日 = new DateOnly(2025, 3, 13)
            }
        };

        // Act
        var result = 出退勤判定Service.判定(対象打刻データ, 周辺打刻データ);

        // Assert
        Assert.Equal("多分出勤", result.ToString());
    }

    [Fact]
    public void 判定_昼_打刻なし_多分出勤を返す()
    {
        // Arrange
        var 対象打刻データ = new 社員打刻
        {
            社員番号 = "001",
            打刻時間 = new DateTime(2025, 3, 13, 13, 0, 0), // 昼13時
            打刻日 = new DateOnly(2025, 3, 13)
        };

        var 周辺打刻データ = new List<社員打刻>(); // 打刻なし

        // Act
        var result = 出退勤判定Service.判定(対象打刻データ, 周辺打刻データ);

        // Assert
        Assert.Equal("多分出勤", result.ToString());
    }

    [Fact]
    public void 判定_昼_打刻あり_多分退勤を返す()
    {
        // Arrange
        var 対象打刻データ = new 社員打刻
        {
            社員番号 = "001",
            打刻時間 = new DateTime(2025, 3, 13, 13, 0, 0), // 昼13時
            打刻日 = new DateOnly(2025, 3, 13)
        };

        var 周辺打刻データ = new List<社員打刻>
        {
            new 社員打刻
            {
                社員番号 = "001",
                打刻時間 = new DateTime(2025, 3, 13, 8, 0, 0), // 朝8時
                打刻日 = new DateOnly(2025, 3, 13)
            }
        };

        // Act
        var result = 出退勤判定Service.判定(対象打刻データ, 周辺打刻データ);

        // Assert
        Assert.Equal("多分退勤", result.ToString());
    }

    [Fact]
    public void 判定_夜_打刻なし_多分退勤を返す()
    {
        // Arrange
        var 対象打刻データ = new 社員打刻
        {
            社員番号 = "001",
            打刻時間 = new DateTime(2025, 3, 13, 18, 0, 0), // 夜18時
            打刻日 = new DateOnly(2025, 3, 13)
        };

        var 周辺打刻データ = new List<社員打刻>(); // 打刻なし

        // Act
        var result = 出退勤判定Service.判定(対象打刻データ, 周辺打刻データ);

        // Assert
        Assert.Equal("多分退勤", result.ToString());
    }

    [Fact]
    public void 判定_夜_打刻あり_退勤を返す()
    {
        // Arrange
        var 対象打刻データ = new 社員打刻
        {
            社員番号 = "001",
            打刻時間 = new DateTime(2025, 3, 13, 18, 0, 0), // 夜18時
            打刻日 = new DateOnly(2025, 3, 13)
        };

        var 周辺打刻データ = new List<社員打刻>
        {
            new 社員打刻
            {
                社員番号 = "001",
                打刻時間 = new DateTime(2025, 3, 13, 8, 0, 0), // 朝8時
                打刻日 = new DateOnly(2025, 3, 13)
            }
        };

        // Act
        var result = 出退勤判定Service.判定(対象打刻データ, 周辺打刻データ);

        // Assert
        Assert.Equal("退勤", result.ToString());
    }
}
