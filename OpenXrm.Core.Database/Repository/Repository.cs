using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenXrm.Core.Core.Abstractions;
using OpenXrm.Core.Database.Infrastructure;
using System.Linq.Expressions;

namespace OpenXrm.Core.Database.Repository
{
    public class Repository<T> where  T: Entity
    {
        internal CoreContext _dbContext;
        internal DbSet<T> _dbSet;

        public Repository(CoreContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetById(Guid id, bool withRelated = true)
        {
            return _dbSet.Find(id);
        }
        public virtual IEnumerable<T> Get(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual async Task Delete(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            await Save();
        }
        public virtual async Task<T> Upsert(T entity)
        {
            var current = _dbSet.Find(entity.Id);
            if (current == null)
            {
                entity.Id = Guid.NewGuid();
                _dbSet.Add(entity);
                await Save();
                return entity;
            }
            _dbContext.Entry(current).State = EntityState.Detached;
            entity.LastChanged = DateTime.UtcNow;
            _dbSet.Update(entity);
            //_dbContext.Entry(entity).State = EntityState.Modified;
            await Save();
            return entity;
        }
        public async Task Save()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

    }
}