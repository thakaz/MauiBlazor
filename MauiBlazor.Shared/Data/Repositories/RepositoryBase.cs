﻿namespace MauiBlazor.Shared.Data.Repositories.Base;

/// <summary>
/// リポジトリの基底クラス
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsync(CompositeKey id);
    Task<T?> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task DeleteAsync(CompositeKey id);
}

/// <summary>
/// リポジトリの基底クラス
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    private readonly IDbContextFactory<出退勤DbContext> _contextFactory;

    public RepositoryBase(IDbContextFactory<出退勤DbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        using var _context = await _contextFactory.CreateDbContextAsync();
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        using var _context = await _contextFactory.CreateDbContextAsync();
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<T?> GetByIdAsync(CompositeKey id)
    {
        using var _context = await _contextFactory.CreateDbContextAsync();
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<T?> AddAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
        }

        using var _context = await _contextFactory.CreateDbContextAsync();
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();

        // エンティティ本体を返す
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
        }

        using var _context = await _contextFactory.CreateDbContextAsync();
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        using var _context = await _contextFactory.CreateDbContextAsync();
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with id {id} not found");
        }

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(CompositeKey id)
    {
        using var _context = await _contextFactory.CreateDbContextAsync();
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}

/// <summary>
/// 複合主キー用のクラス
/// </summary>
public class CompositeKey
{
    public List<object> KeyParts { get; }

    public CompositeKey(params object[] keyParts)
    {
        KeyParts = new List<object>(keyParts);
    }

    public override bool Equals(object? obj)
    {
        if (obj is CompositeKey otherKey)
        {
            return KeyParts.SequenceEqual(otherKey.KeyParts);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(KeyParts);
    }
}
