using Application.FormSubmission;
using Application.Helpers;
using Domain.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.FormSubmission.Queries
{
    public class GetFormSubmissionByIdQueryHandler : IRequestHandler<GetFormSubmissionByIdQuery, ResponseWrapper<FormSubmissionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFormSubmissionByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ResponseWrapper<FormSubmissionDTO>> Handle(GetFormSubmissionByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.FormSubmissionRepository
                .Where(f => f.Id == request.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            return entity != null
                ? new ResponseWrapper<FormSubmissionDTO> { Result = Mappings.ToDTO(entity) }
                : new ResponseWrapper<FormSubmissionDTO>
                {
                    Result = null,
                    ErrorMessage = new ErrorMessage { StatusCode = 404, ErrorMessages = ["Submission not found"] }
                };
        }
    }
}