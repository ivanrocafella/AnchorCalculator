using Core.AnchorCalculator.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ViewModels
{
    public class AnchorViewModel : IValidatableObject
    {

        [Range(200,6000,ErrorMessage = "Укажите длину от 200 до 6000")]
        [Display(Name = "Длина, мм")]
        public int Length { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Выберите материал")]
        [Display(Name = "Диаметр, мм")]
        public float Diameter { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        /// <summary>
        /// Get or set anchhor's billet 
        /// </summary>
        // [Remote("CheckBendLength", "Anchor", ErrorMessage = "Длина загиба должна быть от 100 до 500")]
        [Range(0, 500, ErrorMessage = "Длина загиба должна быть не более 500")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Длина загиба, мм")]
        public int BendLength { get; set; }
        [Display(Name = "Радиус загиба, мм")]
        public int BendRadius { get; set; }
      //  [Remote("CheckThreadLength", "Anchor", ErrorMessage = "Длина резьбы должна быть от 50 до 100")]
        [Range(50, 100, ErrorMessage = "Укажите длину от 50 до 100")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Длина резьбы, мм")]
        public int ThreadLength { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Укажите диаметр резьбы")]
        [Display(Name = "Диаметр резьбы, мм")]
        public int ThreadDiameter { get; set; }
        [Display(Name = "Шаг резьбы, мм")]
        public double ThreadStep { get; set; }
        public double Amount { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Укажите кол-во")]
        [Display(Name = "Кол-во, шт")]
        public int Quantity { get; set; }
        public int TypeProfileId { get; set; }
        public DateTime DateCreate { get; set; }
        public string? Material { get; set; }
        public string? SvgElement { get; set; }
        public double BatchWeight { get; set; }
        public double BilletLength { get; set; }
        [Display(Name = "Материал")]
        public int MaterialId { get; set; }
        public List<Material>? Materials { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new();
            if (Diameter < ThreadDiameter)
                errors.Add(new ValidationResult("Диаметр резьбы должен быть меньше или равен диаметру анкера", new[] { "ThreadDiameter" }));
            return errors;
        }
    }
}
