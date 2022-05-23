using BookStore.Data.Interface;
using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Repository
{
    public class BookRepository : GenericRepository<DataContext, Book>, IBook
    {
        public BookRepository(DataContext context) : base(context)
        {

        }
        public async Task<Book> GetByName(string Name)
        {
            var result = await FindToList(x => x.Name == Name);
            return result.FirstOrDefault();
        }

    }
}
