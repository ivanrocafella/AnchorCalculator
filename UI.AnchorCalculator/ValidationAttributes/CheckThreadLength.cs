using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false, Inherited = true)]
    public class CheckThreadLength : ValidationAttribute
    {
        private readonly string _hasCuttingThreadlPropertyName;

        public CheckThreadLength(string hasCuttingThreadlPropertyName)
        {
            _hasCuttingThreadlPropertyName = hasCuttingThreadlPropertyName;
        }

        //https://makolyte.com/aspnetcore-client-side-custom-validation-attributes/
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return null;
            else
            {
                var treadLength = (int)value;
                if (treadLength < 50 || treadLength > 6000)
                    return new ValidationResult("Укажите длину от 50 до 6000");
            }               
            return ValidationResult.Success;
        }
    }
}
