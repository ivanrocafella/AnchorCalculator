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
        private readonly string _firstLengthName;

        public CheckLength(string hasThreadName, string bendRadiusName, string firstLengthName)
        {
            _hasThreadName = hasThreadName;
            _bendRadiusName = bendRadiusName;
            _firstLengthName = firstLengthName;
        }

        //https://makolyte.com/aspnetcore-client-side-custom-validation-attributes/
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var hasThreadPropertyInfo = validationContext.ObjectType.GetProperty(_hasThreadName);
            var hasThreadPropertyVal = Convert.ToBoolean(hasThreadPropertyInfo.GetValue(validationContext.ObjectInstance));
            var bendRadiusNamePropertyInfo = validationContext.ObjectType.GetProperty(_bendRadiusName);
            var bendRadiusPropertyVal = Convert.ToDouble(bendRadiusNamePropertyInfo.GetValue(validationContext.ObjectInstance));
            double bendRadiusMax = bendRadiusPropertyVal + 60;
            double firstLengthPropertyVal;
            var firstLengthPropertyInfo = validationContext.ObjectType.GetProperty(_firstLengthName);
            if (firstLengthPropertyInfo != null)
                firstLengthPropertyVal = Convert.ToDouble(firstLengthPropertyInfo.GetValue(validationContext.ObjectInstance));
            else
                return new ValidationResult($"Unknown property: {_firstLengthName}");
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return null;
            else
            {
                var length = (int)value;
                if (length > firstLengthPropertyVal)
                    return new ValidationResult($"Длина второго конца должна быть меньше или равна длине первого");
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
