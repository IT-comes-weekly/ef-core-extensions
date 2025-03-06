using System.ComponentModel;
using System.Linq.Expressions;
using ITCW.EfCore.Contracts.Models;

namespace ITCW.EfCore.Contracts.Repositories;

/// <summary>
/// Base Repository to define multiple generic Methods inherited members can use.
/// </summary>
/// <typeparam name="TEntity">Type of the entity class the repository is intended for.</typeparam>
public interface IITCWBaseRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Get all filtered entities of type <see cref="TEntity"/> async.
    /// </summary>
    /// <param name="filterExpression">The expression to filter for the entities to return.</param>
    /// <param name="selector">The selector which specifies all properties to return of the entity.</param>
    /// <param name="orderByExpression">The expression to sort with.</param>
    /// <param name="sortDirection">The sort direction to use.</param>
    /// <param name="skip">The amount of entries to skip.</param>
    /// <param name="take">The amount of results to return.</param>
    /// <param name="asNoTracking">Toggle to execute the statement without change tracking.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> which holds information about the current connection.</param>
    /// <returns>The entities of type <see cref="TEntity"/> as <see cref="List{T}"/>.</returns>
    Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filterExpression = null,
        Expression<Func<TEntity, TEntity>>? selector = null,
        Expression<Func<TEntity, object>>? orderByExpression = null,
        ListSortDirection sortDirection = ListSortDirection.Ascending,
        int? skip = null,
        int? take = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get filtered entities of type <see cref="TEntity"/> as paged result async.
    /// </summary>
    /// <param name="filterExpression">The expression to filter for the entities to return.</param>
    /// <param name="selector">The selector which specifies all properties to return of the entity.</param>
    /// <param name="orderByExpression">The expression to sort with.</param>
    /// <param name="sortDirection">The sort direction to use.</param>
    /// <param name="skip">The amount of entries to skip.</param>
    /// <param name="take">The amount of results to return.</param>
    /// <param name="asNoTracking">Toggle to execute the statement without change tracking.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> which holds information about the current connection.</param>
    /// <returns>The paged entities of type <see cref="TEntity"/>.</returns>
    Task<PagingResponse<TEntity>> GetPagedAsync(
        Expression<Func<TEntity, bool>>? filterExpression = null,
        Expression<Func<TEntity, TEntity>>? selector = null,
        Expression<Func<TEntity, object>>? orderByExpression = null,
        ListSortDirection sortDirection = ListSortDirection.Ascending,
        int? skip = null,
        int? take = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get one filtered entity of type <see cref="TEntity"/> async.
    /// </summary>
    /// <param name="filterExpression">The expression to filter for the entities to return.</param>
    /// <param name="selector">The selector which specifies all properties to return of the entity.</param>
    /// <param name="orderByExpression">The expression to sort with.</param>
    /// <param name="sortDirection">The sort direction to use.</param>
    /// <param name="skip">The amount of entries to skip.</param>
    /// <param name="take">The amount of results to return.</param>
    /// <param name="asNoTracking">Toggle to execute the statement without change tracking.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> which holds information about the current connection.</param>
    /// <returns>The entity of type <see cref="TEntity"/>.</returns>
    Task<TEntity> GetSingleAsync(
        Expression<Func<TEntity, bool>>? filterExpression = null,
        Expression<Func<TEntity, TEntity>>? selector = null,
        Expression<Func<TEntity, object>>? orderByExpression = null,
        ListSortDirection sortDirection = ListSortDirection.Ascending,
        int? skip = null,
        int? take = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Count all filtered entities async.
    /// </summary>
    /// <param name="filterExpression">The expression to filter for the entities to return.</param>
    /// <param name="selector">The selector which specifies all properties to return of the entity.</param>
    /// <param name="orderByExpression">The expression to sort with.</param>
    /// <param name="sortDirection">The sort direction to use.</param>
    /// <param name="skip">The amount of entries to skip.</param>
    /// <param name="take">The amount of results to return.</param>
    /// <param name="asNoTracking">Toggle to execute the statement without change tracking.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> which holds information about the current connection.</param>
    /// <returns>The number of filtered entities as <see cref="int"/>.</returns>
    Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? filterExpression = null,
        Expression<Func<TEntity, TEntity>>? selector = null,
        Expression<Func<TEntity, object>>? orderByExpression = null,
        ListSortDirection sortDirection = ListSortDirection.Ascending,
        int? skip = null,
        int? take = null,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Add an entity of type <see cref="TEntity"/> async.
    /// </summary>
    /// <param name="entity">The entity of type <see cref="TEntity"/> to add.</param>
    /// <param name="saveChanges">Toggle to run SaveChangesAsync after adding the entity to the change tracker.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> which holds information about the current connection.</param>
    /// <returns>The added entity of type <see cref="TEntity"/>.</returns>
    Task<TEntity> AddAsync(
        TEntity entity,
        bool saveChanges = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete one entity of type <see cref="TEntity"/> async.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> which holds information about the current connection.</param>
    /// <returns>An awaitable <see cref="Task"/>.</returns>
    Task DeleteAsync(
        TEntity entity,
        bool saveChanges = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///  Delete multiple entities of type <see cref="TEntity"/> async.
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> which holds information about the current connection.</param>
    /// <returns>An awaitable <see cref="Task"/>.</returns>
    Task DeleteRangeAsync(
        IEnumerable<TEntity> entities,
        bool saveChanges = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Apply all changes from change tracker async.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> which holds information about the current connection.</param>
    /// <returns>An awaitable <see cref="Task"/>.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}