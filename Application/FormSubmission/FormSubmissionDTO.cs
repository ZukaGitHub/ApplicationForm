using Domain.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FormSubmission
{
    public class FormSubmissionDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public bool SubscribeToNewsletter { get; set; }
        public string? Country { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? CountryAddress { get; set; }
        public List<AdditionalPropertiesDTO?>? AdditionalProperties { get; set; }
    }
}
