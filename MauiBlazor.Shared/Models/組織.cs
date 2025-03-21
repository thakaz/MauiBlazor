﻿using System.ComponentModel.DataAnnotations;

namespace MauiBlazor.Shared.Models;

public class 組織
{
    [Key]
    public int Id { get; set; }

    //一意制約はfluentAPIで設定する
    [Required]
    public required string 組織コード { get; set; }

    [Required]
    public string 組織名 { get; set; }

    [Required]
    public bool Is管理組織 { get; set; } = false;

    //パスワードのハッシュ
    public string? パスワード { get; set; }

    public IList<グループ> グループ { get; } = new List<グループ>();

    public IList<社員> 社員 { get; } = new List<社員>();

}

public class グループ
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string グループ名 { get; set; } = "グループなし";
}
