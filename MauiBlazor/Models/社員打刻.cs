using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace MauiBlazor.Models;

class 社員打刻
{
    [PrimaryKey,AutoIncrement]
    public int Id { get; set; }

    public string 社員番号 { get; set; }
    public DateTime 打刻時間 { get; set; }
    public string 備考 { get; set; }
}
