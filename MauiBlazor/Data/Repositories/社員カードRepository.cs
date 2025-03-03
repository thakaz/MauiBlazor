using MauiBlazor.Data.Repositories.Base;
using MauiBlazor.Models;
using Microsoft.EntityFrameworkCore;


namespace MauiBlazor.Data.Repositories;

public interface I社員カードRepository : IRepository<社員カード>
{
    Task<社員カード?> GetByカードUIDAsync(string カードUID);
    Task<IList<社員カード>> GetBy社員番号Async(string 社員番号);
}

public class 社員カードRepository : RepositoryBase<社員カード>, I社員カードRepository
{
    private readonly IDbContextFactory<出退勤DbContext> _contextFactory;

    public 社員カードRepository(IDbContextFactory<出退勤DbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }


    public async Task<社員カード?> GetByカードUIDAsync(string カードUID)
    {
        using var _context = await _contextFactory.CreateDbContextAsync();
        return await _context.Set<社員カード>().FirstOrDefaultAsync(x => x.カードUID == カードUID);
    }

    public override async Task AddAsync(社員カード entity)
    {
        using var _context = await _contextFactory.CreateDbContextAsync();

        entity.社員 = await _context.Set<社員>().FindAsync(entity.社員.Id);

        _context.Set<社員カード>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<社員カード>> GetBy社員番号Async(string 社員番号)
    {
        using var _context = await _contextFactory.CreateDbContextAsync();

        return await _context.Set<社員カード>().Where(x => x.社員.社員番号 == 社員番号)
            .OrderByDescending(x => x.追加日)
            .ToListAsync();
    }
}
