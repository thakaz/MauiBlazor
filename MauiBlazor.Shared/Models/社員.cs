using System.ComponentModel.DataAnnotations;

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
    public required string 名前 { get; set; }
    public string? 備考 { get; set; }

    public IList<社員カード> 社員カード { get; } = new List<社員カード>();

}
