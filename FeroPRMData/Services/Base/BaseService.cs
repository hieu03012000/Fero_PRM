using FeroPRMData.Repository.Base;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FeroPRMData.Services.Base
{
    public interface IBaseService<T> where T: class
    {
        int Count();
        T Get<TKey>(TKey id);
        Task<T> GetAsyn<TKey>(TKey id);
        IQueryable<T> Get();
        T FirstOrDefault();
        Task<T> FirstOrDefaultAsyn();
        void Create(T entity);
        Task CreateAsyn(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsyn(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task RemoveRange(IQueryable<T> entitys);
    }
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected IBaseRepository<T> _repository;
        public BaseService()
        {
        }
        public BaseService(IBaseRepository<T> repository)
        {
            this._repository = repository;
        }

        public int Count()
        {
            return _repository.Count();
        }

        public void Create(T entity)
        {
            _repository.Create(entity);
        }

        public async Task CreateAsyn(T entity)
        {
            await _repository.CreateAsyn(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public T FirstOrDefault()
        {
            return _repository.FirstOrDefault();
        }

        public async Task<T> FirstOrDefaultAsyn()
        {
            return await _repository.FirstOrDefaultAsyn();
        }

        public T Get<TKey>(TKey id)
        {
            return _repository.Get(id);
        }

        public IQueryable<T> Get()
        {
            return _repository.Get();
        }

        public async Task<T> GetAsyn<TKey>(TKey id)
        {
            return await _repository.GetAsyn(id);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _repository.Get(predicate);
        }
        public async Task<T> FirstOrDefaultAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _repository.FirstOrDefaultAsyn(predicate);
        }
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _repository.FirstOrDefault(predicate);
        }

        public async Task RemoveRange(IQueryable<T> entitys)
        {
            await _repository.RemoveRange(entitys);
        }
    }
}
