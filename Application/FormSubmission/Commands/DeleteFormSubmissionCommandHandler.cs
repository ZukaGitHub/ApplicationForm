using Application.Helpers;
using Domain.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FormSubmission.Commands
{
    public class DeleteFormSubmissionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteFormSubmissionCommand, ResponseWrapper<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<ResponseWrapper<bool>> Handle(DeleteFormSubmissionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.FormSubmissionRepository.Where(f => f.Id == request.Id, cancellationToken).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (entity == null)
                return new ResponseWrapper<bool>
                {
                    Result = false,
                    ErrorMessage = new ErrorMessage { StatusCode = 404, ErrorMessages = ["Submission not found"] }
                };

            _unitOfWork.FormSubmissionRepository.Remove(entity);
            var changes = await _unitOfWork.SaveAsync();

            return changes > 0
                ? new ResponseWrapper<bool> { Result = true }
                : new ResponseWrapper<bool>
                {
                    Result = false,
                    ErrorMessage = new ErrorMessage { StatusCode = 400, ErrorMessages = ["Failed to delete submission"] }
                };
        }
    }
}
