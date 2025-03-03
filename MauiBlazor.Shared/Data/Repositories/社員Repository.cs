namespace MauiBlazor.Shared.Data.Repositories;

public interface I社員Repository : IRepository<社員>
{
    Task<社員?> GetByカードUIDAsync(string カードUID);
    Task<社員?> GetBy社員番号Async(string 社員番号);
}

public class 社員Repository : RepositoryBase<社員>, I社員Repository
{
    private readonly IDbContextFactory<出退勤DbContext> _contextFactory;

    public 社員Repository(IDbContextFactory<出退勤DbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<社員?> GetByカードUIDAsync(string カードUID)
    {
        using var _context = await _contextFactory.CreateDbContextAsync();
        return await _context.Set<社員>().FirstOrDefaultAsync(x => x.社員カード.Any(y => y.カードUID == カードUID));
    }

    public async Task<社員?> GetBy社員番号Async(string 社員番号)
    {
        using var _context = await _contextFactory.CreateDbContextAsync();
        return await _context.Set<社員>().FirstOrDefaultAsync(x => x.社員番号 == 社員番号);
    }


}
