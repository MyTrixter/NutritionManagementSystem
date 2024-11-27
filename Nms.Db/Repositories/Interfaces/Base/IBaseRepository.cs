using System.Linq.Expressions;

namespace Nms.Db.Repositories.Interfaces.Base;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllByWhereOrderedAscendingAsync(
        Expression<Func<TEntity, bool>> match,
        Expression<Func<TEntity, object>> orderBy);

    Task<List<TEntity>> GetAllByWhereOrderedDescendingAsync(
       Expression<Func<TEntity, bool>> match,
       Expression<Func<TEntity, object>> orderBy);

    Task<TEntity?> GetFirstWhereAsync(Expression<Func<TEntity, bool>> match);

    Task<List<TEntity>> FindAllByWhereAsync(Expression<Func<TEntity, bool>> match);

    Task<List<TEntity>> GetAllAsync();

    Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entities);

    Task<TEntity> UpdateAsync(TEntity entityToUpdate);

    Task<TEntity> InsertAsync(TEntity entity);

    Task<IList<TEntity>> InsertRangeAsync(IList<TEntity> entities);

    Task Delete(TEntity entity);

    Task DeleteRange(ICollection<TEntity> entities);

    Task<int> Count();

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> match);
}