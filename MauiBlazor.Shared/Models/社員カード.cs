using System.ComponentModel.DataAnnotations;


namespace MauiBlazor.Shared.Models;

public class 社員カード
{
    [Key]
    public required string カードUID { get; set; }

    public required string 社員番号 { get; set; }

    public string? カード名称 { get; set; }
    public string? 備考 { get; set; }
    
    public DateTime 追加日 { get; set; }= DateTime.Now;

    public required  社員 社員 { get; set; }
}
