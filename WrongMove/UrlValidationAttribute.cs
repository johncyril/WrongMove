using System;
using System.ComponentModel.DataAnnotations;

namespace WrongMove
{
    internal class UrlValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!Uri.IsWellFormedUriString(value.ToString(), UriKind.Absolute))
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.DisplayName });
            }
           
            return null;
        }
    }
}