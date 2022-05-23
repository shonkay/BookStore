using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Interface
{
    public interface IGeneric<TContext, T> where TContext : DbContext where T : class
    {
        Task<T> GetById(long id);
        Task<T> GetByGuidId(Guid id);
        Task<T> GetByIntId(int id);
        Task<IQueryable<T>> GetAll();
        Task<IEnumerable<T>> GetAllByPage(int page, int pageSize);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression, int page, int pageSize);
        Task<IEnumerable<T>> FindToList(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
