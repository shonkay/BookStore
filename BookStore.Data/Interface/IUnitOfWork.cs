using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICategory Category { get; }
        IBook Book { get; }
        Task<int> Complete();
        void Cancel();
    }
}
