using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    using Domain.Enitites;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.Json.Nodes;

    public static class FormSubmissionNormalizer
    {
        private static readonly Dictionary<string, string> PropertyMappings = new(StringComparer.OrdinalIgnoreCase)
    {
        { "first_name", "FirstName" }, { "fname", "FirstName" }, { "givenName", "FirstName" }, { "first", "FirstName" },
        { "last_name", "LastName" }, { "lname", "LastName" }, { "surname", "LastName" }, { "familyName", "LastName" },
        { "email_address", "Email" }, { "emailId", "Email" }, { "userEmail", "Email" },
        { "dob", "BirthDate" }, { "birth_date", "BirthDate" }, { "dateOfBirth", "BirthDate" }, { "bday", "BirthDate" },
        { "sex", "Gender" }, { "userGender", "Gender" }, { "genderIdentity", "Gender" },
        { "newsletter", "SubscribeToNewsletter" }, { "isSubscribed", "SubscribeToNewsletter" }, { "newsletterOptIn", "SubscribeToNewsletter" },
        { "country_name", "Country" }, { "nation", "Country" }, { "locationCountry", "Country" },
        { "street_name", "Street" }, { "addressLine1", "Street" }, { "road", "Street" },
        { "city_name", "City" }, { "town", "City" }, { "locality", "City" },
        { "state_name", "State" }, { "province", "State" }, { "region", "State" },
        { "zip_code", "PostalCode" }, { "zipcode", "PostalCode" }, { "postal", "PostalCode" }, { "pincode", "PostalCode" },
        { "fullAddress", "CountryAddress" }, { "addressCountry", "CountryAddress" }, { "addressNation", "CountryAddress" }
    };

        public static Domain.Enitites.FormSubmission Normalize(Dictionary<string, object> submissionData)
        {
            var model = new Domain.Enitites.FormSubmission();
            var additionalProperties = new List<AdditionalProperties>();

            foreach (var (key, value) in submissionData)
            {
                if (PropertyMappings.TryGetValue(key, out var mappedProperty))
                {
                    PropertyInfo? property = typeof(Domain.Enitites.FormSubmission).GetProperty(mappedProperty);
                    if (property != null)
                    {
                        try
                        {
                            property.SetValue(model, Convert.ChangeType(value, property.PropertyType));
                        }
                        catch
                        {
                            additionalProperties.Add(new AdditionalProperties { Key = key, Value = value.ToString() });
                        }
                    }
                }
                else
                {
                    additionalProperties.Add(new AdditionalProperties { Key = key, Value = value.ToString() });
                }
            }

            model.AdditionalProperties = additionalProperties.Any() ? additionalProperties : null;
            return model;
        }
    }
}
