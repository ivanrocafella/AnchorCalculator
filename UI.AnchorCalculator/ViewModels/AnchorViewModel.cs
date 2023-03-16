using Core.AnchorCalculator.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ViewModels
{
    public class AnchorViewModel 
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
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
        [Display(Name = "Длина загиба, мм")]
        public int BendLength { get; set; }
        [Display(Name = "Радиус загиба, мм")]
        public int BendRadius { get; set; }
        [Range(50, 100, ErrorMessage = "Укажите длину от 50 до 100")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Длина резьбы, мм")]
        public int ThreadLength { get; set; }
        [Display(Name = "Диаметр резьбы, мм")]
        public int ThreadDiameter { get; set; }
        [Display(Name = "Шаг резьбы, мм")]
        public double ThreadStep { get; set; }
        public double Amount { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Кол-во не может быть равно 0")]
        [Display(Name = "Кол-во, шт")]
        public int Quantity { get; set; }
        public int TypeProfileId { get; set; }
        public DateTime DateCreate { get; set; }
        public string? Material { get; set; }
        public string? SvgElement { get; set; }
        public double BatchWeight { get; set; }
        public double BilletLength { get; set; }
        public double Sebes { get; set; }
        public double BatchSebes { get; set; }
        public string? UserName { get; set; }
        public string? MaterialName { get; set; }
        [Display(Name = "Материал")]
        public int MaterialId { get; set; }
        public List<Material>? Materials { get; set; }
        public Anchor? Anchor { get; set; }
    }
}
