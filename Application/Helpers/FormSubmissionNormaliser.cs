using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Domain.Enitites;

namespace Application.Helpers
{
    public static class FormSubmissionNormalizer
    {
        private static readonly Dictionary<string, string> PropertyMappings = new(StringComparer.OrdinalIgnoreCase)
        {
            { "first_name", "FirstName" }, { "fname", "FirstName" }, { "givenName", "FirstName" }, { "first", "FirstName" }, { "firstName", "FirstName" },
            { "last_name", "LastName" }, { "lname", "LastName" }, { "surname", "LastName" }, { "familyName", "LastName" }, { "lasName", "LastName" },
            { "email_address", "Email" }, { "emailId", "Email" }, { "userEmail", "Email" }, { "email", "Email" },
            { "dob", "BirthDate" }, { "birth_date", "BirthDate" }, { "dateOfBirth", "BirthDate" }, { "bday", "BirthDate" }, { "birthDate", "BirthDate" },
            { "sex", "Gender" }, { "userGender", "Gender" }, { "genderIdentity", "Gender" }, { "gender", "Gender" },
            { "newsletter", "SubscribeToNewsletter" }, { "isSubscribed", "SubscribeToNewsletter" }, { "newsletterOptIn", "SubscribeToNewsletter" }, { "subscribeToNewsletter", "SubscribeToNewsletter" },
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
                object? normalizedValue = value;
                if (normalizedValue == null)
                {
                    continue;
                }
                if (value is JsonElement element)
                {
                    switch (element.ValueKind)
                    {
                        case JsonValueKind.String:
                            normalizedValue = element.GetString();
                            break;
                        case JsonValueKind.Number:
                            if (element.TryGetInt32(out int intVal))
                                normalizedValue = intVal;
                            else if (element.TryGetDouble(out double doubleVal))
                                normalizedValue = doubleVal;
                            break;
                        case JsonValueKind.True:
                        case JsonValueKind.False:
                            normalizedValue = element.GetBoolean();
                            break;
                        case JsonValueKind.Null:
                            normalizedValue = null;
                            break;
                        default:
                            normalizedValue = element.ToString();
                            break;
                    }
                }

                if (PropertyMappings.TryGetValue(key, out var mappedProperty))
                {
                    PropertyInfo? property = typeof(Domain.Enitites.FormSubmission).GetProperty(mappedProperty);
                    if (property != null)
                    {
                        try
                        {
                            object? convertedValue = ConvertValue(normalizedValue, property.PropertyType);
                            property.SetValue(model, convertedValue);
                        }
                        catch (Exception)
                        {
                            additionalProperties.Add(new AdditionalProperties { Key = key, Value = normalizedValue?.ToString() });
                        }
                    }
                }
                else
                {
                    additionalProperties.Add(new AdditionalProperties { Key = key, Value = normalizedValue?.ToString() });
                }
            }

            model.AdditionalProperties = additionalProperties.Any() || additionalProperties.Count==0 ? additionalProperties : null;
            return model;
        }

        private static object? ConvertValue(object? value, Type targetType)
        {
            if (value == null)
                return null;

            Type underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
            try
            {
                if (underlyingType == typeof(DateTime))
                {
                    if (value is string s && DateTime.TryParse(s, out DateTime date))
                    {
                        return date;
                    }
                    if (value is DateTime dt)
                    {
                        return dt;
                    }
                }
                else if (underlyingType == typeof(bool))
                {
                    if (value is string s)
                    {
                        if (bool.TryParse(s, out bool b))
                            return b;
                        if (int.TryParse(s, out int n))
                            return n != 0;
                    }
                    else if (value is int i)
                    {
                        return i != 0;
                    }
                    else if (value is bool)
                    {
                        return value;
                    }
                }
                // Add additional type handling if necessary

                return Convert.ChangeType(value, underlyingType);
            }
            catch
            {
                return null;
            }
        }
    }
}
