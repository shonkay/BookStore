using BookStore.Data.Interface;
using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Repository
{
    public class CategoryRepository : GenericRepository<DataContext, Category>, ICategory
    {
        public CategoryRepository(DataContext context) : base(context)
        {

        }
        public async Task<Category> GetByName(string CategoryName)
        {
            var result = await FindToList(x => x.Name == CategoryName);
            return result.FirstOrDefault();
        }
    }
}
