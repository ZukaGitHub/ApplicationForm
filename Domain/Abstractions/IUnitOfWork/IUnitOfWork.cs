using Domain.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IFormSubmissionRepository FormSubmissionRepository { get; }
        IAdditionalPropertiesRepository AdditionalPropertiesRepository { get; }
        Task<int> SaveAsync();
        int Save();
    }
}
