using Application.Helpers;
using Domain.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FormSubmission.Queries
{
    public class GetAllFormSubmissionsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllFormSubmissionsQuery, ResponseWrapper<List<FormSubmissionDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ResponseWrapper<List<FormSubmissionDTO>>> Handle(GetAllFormSubmissionsQuery request, CancellationToken cancellationToken)
        {
            var submissions = await _unitOfWork.FormSubmissionRepository
                .Where(_ => true)
                .AsNoTracking()
                .Include(f => f.AdditionalProperties)
                .ToListAsync(cancellationToken);

            var dtoList = submissions.Select(Mappings.ToDTO).ToList();

            return new ResponseWrapper<List<FormSubmissionDTO>> { Result = dtoList };
        }
    }
}
