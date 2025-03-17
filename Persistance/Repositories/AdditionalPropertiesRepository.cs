using Domain.Abstractions.IRepositories;
using Domain.Enitites;
using Persistance.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class AdditionalPropertiesRepository : BaseRepository<AdditionalProperties>, IAdditionalPropertiesRepository
    {
        public AdditionalPropertiesRepository(InMemoryDB.InMemoryDB context) : base(context)
        {
        }
    }
}
