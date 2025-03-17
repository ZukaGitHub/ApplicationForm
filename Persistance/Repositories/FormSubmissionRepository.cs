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
    public class FormSubmissionRepository : BaseRepository<FormSubmission>, IFormSubmissionRepository
    {
        public FormSubmissionRepository(InMemoryDB.InMemoryDB context) : base(context)
        {
        }
    }
}
