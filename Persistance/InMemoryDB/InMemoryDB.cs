using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.InMemoryDB
{
    public class InMemoryDB:DbContext
    {
        public InMemoryDB(DbContextOptions<InMemoryDB> options) : base(options) { }
    }
}
