using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.IUnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
        int Save();
    }
}
