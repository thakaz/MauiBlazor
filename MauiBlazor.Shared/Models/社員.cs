﻿using System.ComponentModel.DataAnnotations;

namespace MauiBlazor.Shared.Models;

public class 社員
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string 社員番号 { get; set; }
    [Required]
    public int 入社年度 { get; set; }
    [Required]
    public required string 名前性 { get; set; }
    [Required]
    public required string 名前名 { get; set; }
    public string? フリガナ性 { get; set; }
    public string? フリガナ名 { get; set; }
    public string? ニックネーム { get; set; }

    public string? メールアドレス { get; set; }



    public string? fullName => $"{名前性} {名前名}";

    public string? 備考 { get; set; }

    [Required]
    public bool Is管理者 { get; set; } = false;

    public IList<社員カード> 社員カード { get; } = new List<社員カード>();

    public 社員設定? 社員設定 { get; set; } = new();

    public IList<グループ> グループ { get; } = new List<グループ>();

    public 組織? 組織 { get; set; }
    public int? 組織Id { get; set; }
}

public class 社員設定
{
    [Key]
    public int Id { get; set; }
    public string? 通知先メールアドレス { get; set; }
    public int 効果音タイプ { get; set; }
    public List<社員メモ> メモ { get; set; } = new();
    public int 社員Id { get; set; }
    public 社員 社員 { get; set; } = null!;
}

public class 社員メモ
{
    [Key]
    public int Id { get; set; }
    public string? タイトル { get; set; }
    public string? 本文 { get; set; }

    public required 社員設定 社員設定 { get; set; }
}
