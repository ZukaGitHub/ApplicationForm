using Domain.Enitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.InMemoryDB
{
    public class InMemoryDB(DbContextOptions<InMemoryDB> options) : DbContext(options)
    {
        DbSet<FormSubmission> FormSubmissions { get; set; }
        DbSet<AdditionalProperties> AdditionalProperties { get; set; }
    }
}
