using Core.AnchorCalculator.Entities;
using Core.AnchorCalculator.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ViewModels
{
    public class AnchorViewModel 
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(200,6000,ErrorMessage = "Укажите длину от 400 до 6000")]
        [Display(Name = "Длина, мм:")]
        public int Length { get; set; }
        [Required(ErrorMessage = "Выберите материал")]
        [Display(Name = "Диаметр, мм:")]
        public string Diameter { get; set; }
        public string? Weight { get; set; }
        public string? Price { get; set; }
        /// <summary>
        /// Get or set anchhor's billet 
        /// </summary>
        [Display(Name = "Длина загиба, мм:")]
        public int BendLength { get; set; }
        [Display(Name = "Радиус загиба, мм:")]
        public int BendRadius { get; set; }
        [Range(50, 100, ErrorMessage = "Укажите длину от 50 до 100")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Длина резьбы, мм:")]
        public int ThreadLength { get; set; }
        [Range(50, 100, ErrorMessage = "Укажите длину от 50 до 100")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Длина резьбы, мм:")]
        public int ThreadLengthSecond { get; set; }
        public bool HasThreadSecond { get; set; }
        [Display(Name = "Диаметр резьбы, мм:")]
        public int ThreadDiameter { get; set; }
        [Display(Name = "Шаг резьбы, мм:")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string ThreadStep { get; set; }
        public string? Amount { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Кол-во не может быть равно 0")]
        [Display(Name = "Кол-во, шт:")]
        public int Quantity { get; set; }
        public int TypeProfileId { get; set; }
        public DateTime DateCreate { get; set; }
        public string? Material { get; set; }
        public string? SvgElement { get; set; }
        public string? BatchWeight { get; set; }
        public string? BilletLength { get; set; }
        public double PriceMaterial { get; set; }
        public double BatchPriceMaterial { get; set; }
        public double LengthPathRoller { get; set; }
        public double LengthBeforeEndPathRoller { get; set; }
        public string? Sebes { get; set; }
        public string? BatchSebes { get; set; }
        public string? UserName { get; set; }
        public string? Kind { get; set; }
        public string? MaterialName { get; set; }
        [Display(Name = "Материал")]
        public int MaterialId { get; set; }
        public List<Material>? Materials { get; set; }
        public Anchor? Anchor { get; set; }
    }
}
