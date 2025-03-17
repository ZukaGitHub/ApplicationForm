using Domain.Abstractions.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InMemoryDB.InMemoryDB _context;

        public UnitOfWork(InMemoryDB.InMemoryDB context)
        {
            _context = context;
        }
        public Task<int> SaveAsync() => _context.SaveChangesAsync();
        public int Save() => _context.SaveChanges();
    }
    }
