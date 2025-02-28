﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiBlazor.Models;

public class 社員カード
{
    [Key]
    public required string カードUID { get; set; }

    public required string 社員番号 { get; set; }

    public required string カード名称 { get; set; }
    public required string 備考 { get; set; }

    public required 社員 社員 { get; set; }
}
