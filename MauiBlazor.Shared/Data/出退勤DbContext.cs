using Microsoft.EntityFrameworkCore.Design;

namespace MauiBlazor.Shared.Data;

public class 出退勤DbContextFactory : IDesignTimeDbContextFactory<出退勤DbContext>
{
    public 出退勤DbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<出退勤DbContext>();
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "出退勤.db");
        //optionsBuilder.UseSqlite($"Data Source={dbPath}");

        optionsBuilder.UseNpgsql(Constants.ConnectionString);

        return new 出退勤DbContext(optionsBuilder.Options);
    }
}


public partial class 出退勤DbContext : DbContext
{

    public 出退勤DbContext(DbContextOptions<出退勤DbContext> options) : base(options)
    {
    }

    public virtual DbSet<社員> 社員s { get; set; } = null!;
    public virtual DbSet<社員カード> 社員カードs { get; set; } = null!;
    public virtual DbSet<社員打刻> 社員打刻s { get; set; } = null!;
    public virtual DbSet<社員設定> 社員設定s { get; set; } = null!;
    public virtual DbSet<社員メモ> 社員メモs { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<社員>()
            .HasIndex(x => x.社員番号)
            .IsUnique();

    }

}