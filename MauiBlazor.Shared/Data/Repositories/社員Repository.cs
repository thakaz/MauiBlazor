﻿namespace MauiBlazor.Shared.Data.Repositories;

public interface I社員Repository : IRepository<社員>
{
    Task<社員?> GetByカードUIDAsync(string カードUID);
    Task<社員?> GetBy社員番号Async(string 社員番号);
    Task<IEnumerable<社員>> Get組織メンバーAsync(string 組織コード);
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
        return await _context.Set<社員>()
            .Include(x => x.社員カード)
            .Include(x => x.社員設定)
            .ThenInclude(y => y.メモ)
            .FirstOrDefaultAsync(x => x.社員番号 == 社員番号);
    }


    public override async Task<社員?> GetByIdAsync(int id)
    {
        using var _context = await _contextFactory.CreateDbContextAsync();
        return await _context.社員s
            .Include(s => s.社員カード)
            .Include(s => s.社員設定)
            .ThenInclude(y => y.メモ)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<社員>> Get組織メンバーAsync(string 組織コード)
    {
        using var _context = await _contextFactory.CreateDbContextAsync();
        return await _context.Set<社員>().Where(x => x.組織 != null && x.組織.組織コード == 組織コード).ToListAsync();
    }

}
