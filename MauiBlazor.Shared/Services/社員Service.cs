﻿namespace MauiBlazor.Shared.Services;


public class 社員Service
{
    private readonly I社員Repository _社員Repository;
    private readonly I社員打刻Repository _社員打刻Repository;
    private readonly I社員カードRepository _社員カードRepository;
    private readonly I通知Service _通知Service;

    public 社員Service(I社員Repository 社員Repository, I社員打刻Repository 社員打刻Repository, I社員カードRepository 社員カードRepository, I通知Service 通知Service)
    {
        _社員Repository = 社員Repository;
        _社員打刻Repository = 社員打刻Repository;
        _社員カードRepository = 社員カードRepository;
        _通知Service = 通知Service;
    }

    public async Task カードの登録(string カードUID, int id)
    {
        var 社員 = await _社員Repository.GetByIdAsync(id);
        if (社員 == null)
        {
            throw new Exception("社員が見つかりませんでした");
        }

        var existingカード = await _社員カードRepository.GetByカードUIDAsync(カードUID);
        if (existingカード != null)
        {
            _通知Service.ShowToast(ToastIntent.Warning, "このカードは既に登録されています" + $"{社員.社員番号} {カードUID}");
            throw new Exception("このカードは既に登録されています");
        }

        var newカード = new 社員カード
        {
            社員番号 = 社員.社員番号,
            カードUID = カードUID,
            社員 = 社員
        };

        await _社員カードRepository.AddAsync(newカード);
        _通知Service.ShowToast(ToastIntent.Success, "カードを追加しました。" + $"{社員.社員番号} {カードUID}");

    }
}
