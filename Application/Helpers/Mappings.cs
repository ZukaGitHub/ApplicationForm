using Application.FormSubmission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    internal class Mappings
    {
        public static FormSubmissionDTO ToDTO(Domain.Enitites.FormSubmission entity)
        {
            return new FormSubmissionDTO
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                BirthDate = entity.BirthDate,
                Gender = entity.Gender,
                SubscribeToNewsletter = entity.SubscribeToNewsletter,
                Country = entity.Country,
                Street = entity.Street,
                City = entity.City,
                State = entity.State,
                PostalCode = entity.PostalCode,
                CountryAddress = entity.CountryAddress,
                AdditionalProperties = entity.AdditionalProperties != null && entity.AdditionalProperties.Count() > 0
                     ? entity.AdditionalProperties.Select(ToDTO).ToList()
                            : null
            };
        }

        public static AdditionalPropertiesDTO ToDTO(Domain.Enitites.AdditionalProperties entity)
        {
            return new AdditionalPropertiesDTO
            {
                Id = entity.Id,
                Key = entity.Key,
                Value = entity.Value
            };
        }
    }
}
