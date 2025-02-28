using System.ComponentModel;
using System.Linq.Expressions;
using ITCW.EfCore.Contracts.Models;
using ITCW.EfCore.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITCW.EfCore.Repositories;

/// <summary>
/// Base Repository to define multiple generic Methods inherited members can use.
/// </summary>
/// <typeparam name="TEntity">Type of the entity class the repository is intended for.</typeparam>
/// <typeparam name="TDbContext">Type of the specific <see cref="DbContext"/> which should be used for communication.</typeparam>
public abstract class ITCWBaseRepository<TEntity, TDbContext> : IITCWBaseRepository<TEntity>
    where TEntity : class, new()
    where TDbContext : DbContext
{
    /// <summary>
    /// Gets the specific <see cref="DbContext"/>.
    /// </summary>
    protected TDbContext Context { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ITCWBaseRepository{TEntity, TDbContext}"/> class.
    /// </summary>
    /// <param name="context">The specific <see cref="DbContext"/> which is used for the communication with the DB.</param>
    protected ITCWBaseRepository(TDbContext context)
    {
        Context = context;
    }
    
    /// <inheritdoc cref="IITCWBaseRepository{TEntity}.GetAsync(Expression{Func{TEntity, bool}}, Expression{Func{TEntity, TEntity}}, Expression{Func{TEntity, object}}, ListSortDirection, int?, int?, bool, CancellationToken)"/>
    public async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filterExpression = null,
        Expression<Func<TEntity, TEntity>>? selector = null,
        Expression<Func<TEntity, object>>? orderByExpression = null,
        ListSortDirection sortDirection = ListSortDirection.Ascending,
        int? skip = null,
        int? take = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = PrepareQuery(filterExpression, selector, orderByExpression, sortDirection, skip, take, asNoTracking);
        
        return await query.ToListAsync(cancellationToken);
    }
    
    /// <inheritdoc cref="IITCWBaseRepository{TEntity}.GetPagedAsync(Expression{Func{TEntity, bool}}, Expression{Func{TEntity, TEntity}}, Expression{Func{TEntity, object}}, ListSortDirection, int?, int?, bool, CancellationToken)"/>
    public async Task<PagingResponse<TEntity>> GetPagedAsync(
        Expression<Func<TEntity, bool>>? filterExpression = null,
        Expression<Func<TEntity, TEntity>>? selector = null,
        Expression<Func<TEntity, object>>? orderByExpression = null,
        ListSortDirection sortDirection = ListSortDirection.Ascending,
        int? skip = null,
        int? take = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = PrepareQuery(filterExpression, selector, orderByExpression, sortDirection, skip, take, asNoTracking);

        var dataTask = query.ToListAsync(cancellationToken);
        var countTask = query.CountAsync(cancellationToken);
        
        await Task.WhenAll(dataTask, countTask);
        
        return new PagingResponse<TEntity>()
        {
            Data = dataTask.Result,
            Count = countTask.Result,
        };
    }

    /// <inheritdoc cref="IITCWBaseRepository{TEntity}.GetSingleAsync(Expression{Func{TEntity, bool}}, Expression{Func{TEntity, TEntity}}, Expression{Func{TEntity, object}}, ListSortDirection, int?, int?, bool, CancellationToken)"/>
    public async Task<TEntity> GetSingleAsync(
        Expression<Func<TEntity, bool>>? filterExpression = null,
        Expression<Func<TEntity, TEntity>>? selector = null,
        Expression<Func<TEntity, object>>? orderByExpression = null,
        ListSortDirection sortDirection = ListSortDirection.Ascending,
        int? skip = null,
        int? take = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = PrepareQuery(filterExpression, selector, orderByExpression, sortDirection, skip, take, asNoTracking);
        
        return await query.SingleAsync(cancellationToken);
    }
    
    /// <inheritdoc cref="IITCWBaseRepository{TEntity}.CountAsync(Expression{Func{TEntity, bool}}, Expression{Func{TEntity, TEntity}}, Expression{Func{TEntity, object}}, ListSortDirection, int?, int?, bool, CancellationToken)"/>
    public async Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? filterExpression = null,
        Expression<Func<TEntity, TEntity>>? selector = null,
        Expression<Func<TEntity, object>>? orderByExpression = null,
        ListSortDirection sortDirection = ListSortDirection.Ascending,
        int? skip = null,
        int? take = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = PrepareQuery(filterExpression, selector, orderByExpression, sortDirection, skip, take, asNoTracking);
        
        return await query.CountAsync(cancellationToken);
    }

    /// <inheritdoc cref="IITCWBaseRepository{TEntity}.AddAsync(TEntity, bool, CancellationToken)"/>
    public async Task<TEntity> AddAsync(
        TEntity entity,
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var entry = Context.Set<TEntity>().Add(entity);

        if (saveChanges)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
        
        return entry.Entity;
    }

    /// <inheritdoc cref="IITCWBaseRepository{TEntity}.DeleteAsync(TEntity, bool, CancellationToken)"/>
    public async Task DeleteAsync(
        TEntity entity,
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        await DeleteRangeAsync(new[] { entity }, saveChanges, cancellationToken);
    }
    
    /// <inheritdoc cref="IITCWBaseRepository{TEntity}.DeleteRangeAsync(IEnumerable{TEntity}, bool, CancellationToken)"/>
    public async Task DeleteRangeAsync(
        IEnumerable<TEntity> entities,
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>().RemoveRange(entities);

        if (saveChanges)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
    
    /// <inheritdoc cref="IITCWBaseRepository{TEntity}.SaveChangesAsync(CancellationToken)"/>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }
    
    private IQueryable<TEntity> PrepareQuery(
        Expression<Func<TEntity, bool>>? filterExpression = null,
        Expression<Func<TEntity, TEntity>>? selector = null,
        Expression<Func<TEntity, object>>? orderByExpression = null,
        ListSortDirection sortDirection = ListSortDirection.Ascending,
        int? skip = null,
        int? take = null,
        bool asNoTracking = true)
    {
        var query = Context.Set<TEntity>().AsQueryable();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }
            
        if (filterExpression != null)
        {
            query = query.Where(filterExpression);
        }
            
        if (orderByExpression != null)
        {
            query = sortDirection == ListSortDirection.Ascending ? query.OrderBy(orderByExpression) : query.OrderByDescending(orderByExpression);
        }
            
        if (selector != null)
        {
            query = query.Select(selector);
        }

        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }
            
        return query;
    }
}