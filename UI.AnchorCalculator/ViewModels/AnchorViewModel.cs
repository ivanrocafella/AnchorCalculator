using Core.AnchorCalculator.Entities;
using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ViewModels
{
    public class AnchorViewModel
    {

        [Range(1,Int32.MaxValue,ErrorMessage = "Длина должна быть не меньше 1")]
        [Display(Name = "Длина")]
        public int Length { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Выберите материал")]
        [Display(Name = "Диаметр")]
        public float Diameter { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        /// <summary>
        /// Get or set anchhor's billet 
        /// </summary>
        public int BendLength { get; set; }
        public int BendRadius { get; set; }
        public int ThreadLength { get; set; }
        public int ThreadDiameter { get; set; }
        public double ThreadStep { get; set; }
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public string? TypeProfile { get; set; }
        public DateTime DateCreate { get; set; }
        public string? SvgPath { get; set; }
        public string? Material { get; set; }
        public double BatchWeight { get; set; }
        public double BilletLength { get; set; }
        [Display(Name = "Материал")]
        public int MaterialId { get; set; }
        public List<Material>? Materials { get; set; }
    }
}
