using Domain.Abstractions.IRepositories;
using Domain.Abstractions.IUnitOfWork;
using Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.UnitOfWork
{
    public class UnitOfWork(InMemoryDB.InMemoryDB context) : IUnitOfWork
    {
        private readonly InMemoryDB.InMemoryDB _context = context;
        private IFormSubmissionRepository formSubmissionRepository;
        private IAdditionalPropertiesRepository additionalPropertiesRepository;
        public IFormSubmissionRepository FormSubmissionRepository => formSubmissionRepository ?? new FormSubmissionRepository(_context);
        public IAdditionalPropertiesRepository AdditionalPropertiesRepository => additionalPropertiesRepository ?? new AdditionalPropertiesRepository(_context);
        public Task<int> SaveAsync() => _context.SaveChangesAsync();
        public int Save() => _context.SaveChanges();
    }
    }
