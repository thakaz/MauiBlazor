using MauiBlazor.Shared.Data.Repositories;
using MauiBlazor.Shared.Models;
using Microsoft.FluentUI.AspNetCore.Components;


namespace MauiBlazor.Shared.Services;

public class 打刻Service
{
    I社員Repository _社員Repository;
    I社員打刻Repository _社員打刻Repository;
    I通知Service _通知Service;

    public 打刻Service(I社員Repository 社員Repository, I社員打刻Repository 社員打刻Repository,I通知Service 通知Service)
    {
        _社員Repository = 社員Repository;
        _社員打刻Repository = 社員打刻Repository;
        _通知Service = 通知Service;
    }

    public async Task 打刻byIDm(string idm)
    {
        await 打刻ByIDm(idm, DateTime.Now);
    }

    //idmから社員番号を取得し、打刻処理を行う
    public async Task 打刻ByIDm(string idm, DateTime dateTime)
    {
        //社員番号を取得
        var 社員 = await _社員Repository.GetByカードUIDAsync(idm);
        //打刻処理
        if (社員 != null)
        {
            await 打刻(社員.社員番号, DateTime.Now);
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

        打刻データ.備考 = 出退勤判定Service.判定(打刻データ, 周辺打刻).ToString();

        await _社員打刻Repository.AddAsync(打刻データ);

        _通知Service.ShowToast(ToastIntent.Success, "打刻しました"+ $"{社員番号} {dateTime}");

    }

}

