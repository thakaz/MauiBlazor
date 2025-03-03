using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiBlazor.Shared.Services;

public class 天気Service
{
    public string 都市名 { get; set; } = "nagoya";
    public string APIキー { get; set; } = "APIキーを設定してください。";

    private Dictionary<DateOnly, bool> 傘が必要 { get; set; } = new Dictionary<DateOnly, bool>();

    public async Task<bool> 傘が必要か(DateOnly date)
    {
        if (!傘が必要.ContainsKey(date))
        {
            await Get天気Async();
        }
        if (!傘が必要.ContainsKey(date))
        {
            return false;
        }
        return 傘が必要[date];
    }

    public 天気Service()
    {
    }

    public async Task<string> Get天気Async()
    {
        return await Get天気Async(this.都市名);
    }

    public async Task<string> Get天気Async(string 都市名)
    {
        try
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={都市名}&appid={APIキー}&lang=ja&units=metric";

            //var url = $"https://api.openweathermap.org/data/2.5/weather?q={都市名}&appid={APIキー}&lang=ja";
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            天気? tenki = JsonSerializer.Deserialize<天気>(json);

            if (tenki != null && tenki.list != null)
            {
                var firstFiveItems = tenki.list.Take(5).ToList();
                // ここで firstFiveItems を使用して必要な処理を行います
                var isRainOrSnow = firstFiveItems.Any(x =>
                {
                    return x.weather.First().main == "Rain" || x.weather.First().main == "Snow";
                });

                if (!傘が必要.Any(z => z.Key == DateOnly.FromDateTime(DateTime.Now)))
                {
                    傘が必要.Add(DateOnly.FromDateTime(DateTime.Now), isRainOrSnow);
                }
            }

            return json;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
