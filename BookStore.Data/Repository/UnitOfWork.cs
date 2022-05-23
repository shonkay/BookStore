using BookStore.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context, ICategory category, IBook book)
        {
            _context = context;
            Category = category;
            Book = book;
        }

        public ICategory Category { get; private set; }
        public IBook Book { get; private set; }
        public async Task<int> Complete() => await _context.SaveChanges();
        public async void Cancel() => await _context.DisposeAsync();
        public void Dispose() => _context.Dispose();
    }
}
