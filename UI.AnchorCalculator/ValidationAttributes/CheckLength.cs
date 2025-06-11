using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false, Inherited = true)]
    public class CheckLength : ValidationAttribute
    {
        private readonly string _hasThreadName;
        private readonly string _bendRadiusName;

        public CheckLength(string hasThreadName, string bendRadiusName)
        {
            _hasThreadName = hasThreadName;
            _bendRadiusName = bendRadiusName;
        }

        //https://makolyte.com/aspnetcore-client-side-custom-validation-attributes/
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var hasThreadPropertyInfo = validationContext.ObjectType.GetProperty(_hasThreadName);
            var hasThreadPropertyVal = Convert.ToBoolean(hasThreadPropertyInfo.GetValue(validationContext.ObjectInstance));
            var bendRadiusNamePropertyInfo = validationContext.ObjectType.GetProperty(_bendRadiusName);
            var bendRadiusPropertyVal = Convert.ToDouble(bendRadiusNamePropertyInfo.GetValue(validationContext.ObjectInstance));
            double bendRadiusMax = bendRadiusPropertyVal + 60;
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return null;
            else
            {
                var length = (int)value;
                if (hasThreadPropertyVal)
                {
                    //if (length < 400 || length > 6000)
                    //    return new ValidationResult("Укажите длину от 400 до 6000");
                    if (length < 400)
                        return new ValidationResult("Укажите длину от 400");
                }
                else
                {
                    //if (length < bendRadiusMax || length > 6000)
                    //    return new ValidationResult($"Укажите длину от {bendRadiusMax} до 6000");
                    if (length < bendRadiusMax)
                        return new ValidationResult($"Укажите длину от {bendRadiusMax}");
                }
            }               
            return ValidationResult.Success;
        }
    }
}
