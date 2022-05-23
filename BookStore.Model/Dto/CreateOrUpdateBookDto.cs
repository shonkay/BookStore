using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model.Dto
{
    public class CreateOrUpdateBookDto
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string Status { get; set; }
        public Guid CategoryId { get; set; }
    }
}
