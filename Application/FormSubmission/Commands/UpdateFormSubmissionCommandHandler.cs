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
    public class UpdateFormSubmissionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateFormSubmissionCommand, ResponseWrapper<FormSubmissionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<ResponseWrapper<FormSubmissionDTO>> Handle(UpdateFormSubmissionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.FormSubmissionRepository.Where(f => f.Id == request.Id,cancellationToken).FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
                return new ResponseWrapper<FormSubmissionDTO>
                {
                    Result = null,
                    ErrorMessage = new ErrorMessage { StatusCode = 404, ErrorMessages = ["Submission not found"] }
                };

            var updatedModel = FormSubmissionNormalizer.Normalize(request.UpdatedData);

            entity.FirstName = updatedModel.FirstName;
            entity.LastName = updatedModel.LastName;
            entity.Email = updatedModel.Email;
            entity.BirthDate = updatedModel.BirthDate;
            entity.Gender = updatedModel.Gender;
            entity.SubscribeToNewsletter = updatedModel.SubscribeToNewsletter;
            entity.Country = updatedModel.Country;
            entity.Street = updatedModel.Street;
            entity.City = updatedModel.City;
            entity.State = updatedModel.State;
            entity.PostalCode = updatedModel.PostalCode;
            entity.CountryAddress = updatedModel.CountryAddress;
            entity.AdditionalProperties = updatedModel.AdditionalProperties;

            var changes = await _unitOfWork.SaveAsync();

            return changes > 0
                ? new ResponseWrapper<FormSubmissionDTO> { Result = Mappings.ToDTO(entity) }
                : new ResponseWrapper<FormSubmissionDTO>
                {
                    Result = null,
                    ErrorMessage = new ErrorMessage { StatusCode = 400, ErrorMessages = ["Update failed"] }
                };
        }
    }
}
