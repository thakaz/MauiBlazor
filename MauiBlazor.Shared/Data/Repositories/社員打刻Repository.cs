namespace MauiBlazor.Shared.Data.Repositories;

public interface I社員打刻Repository : IRepository<社員打刻>
{
    Task<List<社員打刻>> GetBy社員番号Async(string 社員番号);
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

}
