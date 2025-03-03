using System.ComponentModel.DataAnnotations;

namespace MauiBlazor.Shared.Models;

public class 社員打刻
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required string 社員番号 { get; set; }
    public DateTime 打刻時間 { get; set; }
    public DateOnly 打刻日 { get; set; }
    public string? 備考 { get; set; }
}
