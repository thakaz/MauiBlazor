using MauiBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazor.Services;

enum 出退勤判定
{
    出勤,
    退勤,
    多分出勤,
    多分退勤,
    不明
}

static class 出退勤判定Service
{
    public static 出退勤判定 判定(社員打刻 対象打刻データ, List<社員打刻> 周辺打刻データ)
    {

        TimeOnly 一日start = new TimeOnly(5, 00); //出社時間開始

        TimeOnly  出勤start = new TimeOnly(7, 00).AddHours(-5); //出社時間開始
        TimeOnly 出勤end = new TimeOnly(11, 00).AddHours(-5);


        TimeOnly 退勤start = new TimeOnly(16, 00).AddHours(-5); 
        TimeOnly 退勤end = new TimeOnly(20, 00).AddHours(-5);

        var target打刻時間 = new TimeOnly(対象打刻データ.打刻時間.Hour, 対象打刻データ.打刻時間.Minute, 対象打刻データ.打刻時間.Second);

                //出社時間内の場合は、すでに打刻済み以外の場合は出社


    }

}
