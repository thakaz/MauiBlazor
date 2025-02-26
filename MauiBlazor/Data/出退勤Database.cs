using MauiBlazor.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazor.Data;

class 出退勤Database
{
    SQLiteAsyncConnection Database;

    public 出退勤Database(string dbPath)
    {
        Database = new SQLiteAsyncConnection(dbPath);
        Database.CreateTableAsync<社員>().Wait();
        Database.CreateTableAsync<社員打刻>().Wait();
    }

    async Task Init()
    {
        if (Database is not null)
        {
            return;
        }

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<社員>();
        result = await Database.CreateTableAsync<社員打刻>();
    }

    public async Task<List<社員>> Get社員Async()
    {
        await Init();
        return await Database.Table<社員>().ToListAsync();
    }

    public async Task<社員> Get社員Async(string 社員番号)
    {
        await Init();
        return await Database.Table<社員>().Where(i => i.社員番号 == 社員番号).FirstOrDefaultAsync();
    }

    public async Task<int> Add社員Async(社員 item)
    {
        await Init();

        return await Database.InsertAsync(item);
    }

    public async Task<int> Add社員打刻Async(社員打刻 item)
    {
        await Init();
        return await Database.InsertAsync(item);
    }

    public async Task<List<社員打刻>> Get社員打刻Async(string 社員番号)
    {
        await Init();
        return await Database.Table<社員打刻>()
            .Where(i => i.社員番号 == 社員番号)
            .OrderByDescending(i => i.打刻時間)
            .ToListAsync();
    }

    //社員打刻の削除
    public async Task Delete社員打刻Async(int id)
    {
        await Init();
        //データの削除
        await Database.DeleteAsync<社員打刻>(id);

    }

}
