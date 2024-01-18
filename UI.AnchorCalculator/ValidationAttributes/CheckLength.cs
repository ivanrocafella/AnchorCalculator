using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false, Inherited = true)]
    public class CheckLength : ValidationAttribute
    {
        private readonly string _onHydraulicMachinelPropertyName;

        public CheckLength(string onHydraulicMachinelPropertyName)
        {
            _onHydraulicMachinelPropertyName = onHydraulicMachinelPropertyName;
        }

        //https://makolyte.com/aspnetcore-client-side-custom-validation-attributes/
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var onHydraulicMachinelPropertyInfo = validationContext.ObjectType.GetProperty(_onHydraulicMachinelPropertyName);
            var onHydraulicMachinelPropertyVal = Convert.ToBoolean(onHydraulicMachinelPropertyInfo.GetValue(validationContext.ObjectInstance));
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return null;
            else
            {
                var length = (int)value;
                if (onHydraulicMachinelPropertyVal)
                {
                    if (length < 1000 || length > 6000)
                        return new ValidationResult("Укажите длину от 1000 до 6000");
                }
                else
                {
                    if (length < 400 || length > 6000)
                        return new ValidationResult("Укажите длину от 400 до 6000");
                }
            }               
            return ValidationResult.Success;
        }
    }
}
