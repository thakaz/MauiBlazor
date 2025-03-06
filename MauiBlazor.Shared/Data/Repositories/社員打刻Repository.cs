namespace MauiBlazor.Shared.Data.Repositories;

public interface I社員打刻Repository : IRepository<社員打刻>
{
    Task<List<社員打刻>> GetBy社員番号Async(string 社員番号);
    Task<List<社員打刻>> GetBy社員番号And月度Async(string 社員番号, DateOnly 月度);
    Task<List<社員打刻>> GetBy社員番号And年月Async(string 社員番号, DateOnly 年月);
}

public class 社員打刻Repository : RepositoryBase<社員打刻>, I社員打刻Repository
{
    private readonly IDbContextFactory<出退勤DbContext> _contextFactory;

    public 社員打刻Repository(IDbContextFactory<出退勤DbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }



    public async Task<List<社員打刻>> GetBy社員番号Async(string 社員番号)
    {
        using var _context = await _contextFactory.CreateDbContextAsync();
        return await _context.Set<社員打刻>().Where(x => x.社員番号 == 社員番号)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    //指定した月度の打刻データを取得
    public async Task<List<社員打刻>> GetBy社員番号And月度Async(string 社員番号, DateOnly 月度)
    {
        var 月度範囲 = DateUtils.GetMonthRange(月度);

        using var _context = await _contextFactory.CreateDbContextAsync();
        return await _context.Set<社員打刻>().Where(x => x.社員番号 == 社員番号 && x.打刻日>= 月度範囲.firstDay&& x.打刻日<= 月度範囲.lastDay)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    //指定した年月の打刻データを取得
    public async Task<List<社員打刻>> GetBy社員番号And年月Async(string 社員番号, DateOnly 年月)
    {

        var 年月範囲 = (firstDay: new DateOnly(年月.Year, 年月.Month, 1), lastDay: new DateOnly(年月.Year, 年月.Month, DateTime.DaysInMonth(年月.Year, 年月.Month)));

        using var _context = await _contextFactory.CreateDbContextAsync();
        return await _context.Set<社員打刻>().Where(x => x.社員番号 == 社員番号 && x.打刻日 >= 年月範囲.firstDay && x.打刻日 <= 年月範囲.lastDay)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

}
