using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Interface
{
    public interface IBook : IGeneric<DataContext, Book>
    {
        Task<Book> GetByName(string Name);
    }
}
