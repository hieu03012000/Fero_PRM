using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FeroPRMData.Repository.Base
{
    public interface IBaseRepository<T> where T : class
    {
        int Count();
        T Get<TKey>(TKey id);
        Task<T> GetAsyn<TKey>(TKey id);
        IQueryable<T> Get();
        T FirstOrDefault();
        Task<T> FirstOrDefaultAsyn();
        void Create(T entity);
        Task CreateAsyn(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveRange(IQueryable<T> entity);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsyn(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

    }
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DbContext _dbContext;

        protected DbSet<T> _dbSet;
        public BaseRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = this._dbContext.Set<T>();
        }
        public int Count()
        {
            return _dbSet.Count();
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public async Task CreateAsyn(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();

        }

        public T FirstOrDefault()
        {
            return this._dbSet.FirstOrDefault();
        }

        public async Task<T> FirstOrDefaultAsyn()
        {
            return await this._dbSet.FirstOrDefaultAsync();
        }

        public T Get<TKey>(TKey id)
        {
            return (T)this._dbSet.Find(new object[1] { id });
        }

        public IQueryable<T> Get()
        {
            return _dbSet;
        }

        public async Task<T> GetAsyn<TKey>(TKey id)
        {
            return await this._dbSet.FindAsync(new object[1] { id });
        }

/*        public void Update(T entity, Expression<Func<T, bool>> predicate)
        {
            _dbSet.Find(predicate).Update<T>(entity);
        }*/

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.Where(predicate);
        } 
        public async Task<T> FirstOrDefaultAsyn(Expression<Func<T, bool>> predicate)
        {
            return await this._dbSet.FirstOrDefaultAsync(predicate);
        }
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.FirstOrDefault(predicate);
        }

        public async Task RemoveRange(IQueryable<T> entitys)
        {
            _dbSet.RemoveRange(entitys);
            await _dbContext.SaveChangesAsync();
        }
    }
        
}
