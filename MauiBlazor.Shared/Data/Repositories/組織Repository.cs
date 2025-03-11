using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazor.Shared.Data.Repositories;

public interface I組織Repository : IRepository<組織>
{
    public Task<組織?> GetBy組織コードAsync(string 組織コード);
}

public class 組織Repository :  RepositoryBase<組織>, I組織Repository
{
    private readonly IDbContextFactory<出退勤DbContext> _contextFactory;
    public 組織Repository(IDbContextFactory<出退勤DbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }


    public async Task<組織?> GetBy組織コードAsync(string 組織コード)
    {
        using var _context = await _contextFactory.CreateDbContextAsync();
        return await _context.Set<組織>().FirstOrDefaultAsync(x => x.組織コード == 組織コード);
    }

}
