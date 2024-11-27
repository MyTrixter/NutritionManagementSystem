using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nms.Db.Repositories.Interfaces.Base;

namespace Nms.Db.Repositories.Base;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly NmsContext _context;
    private readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(NmsContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<List<TEntity>> GetAllByWhereOrderedAscendingAsync(
        Expression<Func<TEntity, bool>> match,
        Expression<Func<TEntity, object>> orderBy)
    {
        return await _context.Set<TEntity>().Where(match).OrderBy(orderBy).ToListAsync();
    }

    public async Task<List<TEntity>> GetAllByWhereOrderedDescendingAsync(
        Expression<Func<TEntity, bool>> match,
        Expression<Func<TEntity, object>> orderBy)
    {
        return await _context.Set<TEntity>().Where(match).OrderByDescending(orderBy).ToListAsync();
    }

    public async Task Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRange(ICollection<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task<int> Count()
    {
        return await _context.Set<TEntity>().CountAsync();
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<List<TEntity>> FindAllByWhereAsync(Expression<Func<TEntity, bool>> match)
    {
        return await _context.Set<TEntity>().Where(match).ToListAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> match)
    {
        return await _context.Set<TEntity>().AnyAsync(match);
    }

    public virtual async Task<TEntity?> GetFirstWhereAsync(Expression<Func<TEntity, bool>> match)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(match);
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<IList<TEntity>> InsertRangeAsync(IList<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);

        await _context.SaveChangesAsync();

        return entities;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
    {
        try
        {
            if (_context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToUpdate);
            }

            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            await _context.SaveChangesAsync();

            return entityToUpdate;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e.InnerException);
        }
    }

    public virtual async Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entities)
    {
        var detachedEntities = new List<TEntity>();
        _context.ChangeTracker.AutoDetectChangesEnabled = false;
        foreach (var entity in entities)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                detachedEntities.Add(entity);
            }

            _context.Entry(entity).State = EntityState.Modified;
        }

        _dbSet.AttachRange(detachedEntities);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.AutoDetectChangesEnabled = true;

        return entities;
    }
}