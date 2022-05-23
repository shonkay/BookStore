using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Interface
{
    public interface ICategory : IGeneric<DataContext, Category>
    {
        Task<Category> GetByName(string CategoryName);
    }
}
