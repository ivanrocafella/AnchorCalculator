using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false, Inherited = true)]
    public class CheckThreadLength : ValidationAttribute
    {
        private readonly string _diameterMaterialPropertyName;
        private readonly string _hasCuttingThreadlPropertyName;
        private readonly string _onHydraulicMachinelPropertyName;

        public CheckThreadLength(string diameterMaterialPropertyName, string hasCuttingThreadlPropertyName, string onHydraulicMachinelPropertyName)
        {
            _diameterMaterialPropertyName = diameterMaterialPropertyName;
            _hasCuttingThreadlPropertyName = hasCuttingThreadlPropertyName;
            _onHydraulicMachinelPropertyName = onHydraulicMachinelPropertyName;
        }

        //https://makolyte.com/aspnetcore-client-side-custom-validation-attributes/
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var diameterMaterialPropertyInfo = validationContext.ObjectType.GetProperty(_diameterMaterialPropertyName);
            var hasCuttingThreadlPropertyInfo = validationContext.ObjectType.GetProperty(_hasCuttingThreadlPropertyName);
            var onHydraulicMachinelPropertyInfo = validationContext.ObjectType.GetProperty(_onHydraulicMachinelPropertyName);
            var diameterMaterialPropertyVal = Convert.ToDouble(diameterMaterialPropertyInfo.GetValue(validationContext.ObjectInstance));
            var hasCuttingThreadlPropertyVal = Convert.ToBoolean(hasCuttingThreadlPropertyInfo.GetValue(validationContext.ObjectInstance));
            var onHydraulicMachinelPropertyVal = Convert.ToBoolean(onHydraulicMachinelPropertyInfo.GetValue(validationContext.ObjectInstance));
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return null;
            else
            {
                var treadLength = (int)value;
                if (!hasCuttingThreadlPropertyVal)
                {
                    if (onHydraulicMachinelPropertyVal)
                    {
                        if (diameterMaterialPropertyVal <= 32)
                        {
                            if (treadLength < 50 || treadLength > 300)
                                return new ValidationResult("Укажите длину от 50 до 300");
                        }
                        else if (diameterMaterialPropertyVal > 32 && diameterMaterialPropertyVal <= 36)
                        {
                            if (treadLength < 50 || treadLength > 250)   
                                return new ValidationResult("Укажите длину от 50 до 250");
                        }
                        else
                        {
                            if (treadLength < 50 || treadLength > 150)
                                return new ValidationResult("Укажите длину от 50 до 150");
                        }
                    }
                    else
                    {
                        if (treadLength < 50 || treadLength > 150)
                            return new ValidationResult("Укажите длину от 50 до 150");
                    }
                }
            }               
            return ValidationResult.Success;
        }
    }
}
