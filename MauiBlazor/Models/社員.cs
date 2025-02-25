using SQLite;

namespace MauiBlazor.Models;

class 社員
{
    [PrimaryKey] 
    public string 社員番号 { get; set; }
    public int 入社年度 { get; set; }
    public string 名前 { get; set; }
    public string 備考 { get; set; }

}
