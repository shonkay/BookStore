using BookStore.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Repository
{
    public class GenericRepository<TContext, T> : IGeneric<TContext, T> where TContext : DbContext where T : class
    {
        private readonly TContext _context;

        public GenericRepository(TContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression, int page, int pageSize)
        {
            return await _context.Set<T>().Where(expression).Skip(page).Take(pageSize).ToListAsync();

        }

        public async Task<IEnumerable<T>> FindToList(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();

        }

        public async Task<IQueryable<T>> GetAll()
        {
            return  _context.Set<T>().AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllByPage(int page, int pageSize)
        {
            return await _context.Set<T>().Skip(page).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetByGuidId(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetById(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIntId(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
