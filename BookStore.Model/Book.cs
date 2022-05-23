using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    public class Book : BaseModel
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public int BorrowedCount { get; set; }
        public Category Category { get; set; }
        public string Status { get; set; }
        public bool IsFavorite { get; set; }

    }
}
