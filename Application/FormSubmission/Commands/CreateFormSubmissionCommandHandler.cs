using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Helpers;
using Domain.Abstractions.IUnitOfWork;
using MediatR;

namespace Application.FormSubmission.Commands
{
    public class CreateFormSubmissionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateFormSubmissionCommand, ResponseWrapper<int?>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<ResponseWrapper<int?>> Handle(CreateFormSubmissionCommand request, CancellationToken cancellationToken)
        {
            if (request.Inputs == null || request.Inputs.Count == 0)
                return new ResponseWrapper<int?>
                {
                    Result = null,
                    ErrorMessage = new ErrorMessage() { StatusCode = 400, ErrorMessages = ["No inputs were provided"] }
                };

            var normalizedModel = FormSubmissionNormalizer.Normalize(request.Inputs);
            if(normalizedModel.AdditionalProperties==null || normalizedModel.AdditionalProperties?.Count==0)
            {
                normalizedModel.AdditionalProperties = null;
            }
            await _unitOfWork.FormSubmissionRepository.AddAsync(normalizedModel, cancellationToken);
            var changes = await _unitOfWork.SaveAsync();

            return changes > 0
                ? new ResponseWrapper<int?> { Result = normalizedModel.Id }
                : new ResponseWrapper<int?>
                {
                    Result = null,
                    ErrorMessage = new ErrorMessage() { StatusCode = 400, ErrorMessages = ["Data could not be saved"] }
                };
        }
    }
}