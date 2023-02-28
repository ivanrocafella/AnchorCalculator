using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ViewModels
{
    public class MaterialViewModelForEdit
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Название обязательно для заполнения")]
        [Display(Name = "Название")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Размер обязателен для заполнения")]
        [Display(Name = "Размер")]
        public double Size { get; set; }
        [Required(ErrorMessage = "Тип сечения обязателен для заполнения")]
        [Display(Name = "Тип сечения")]
        public int TypeId { get; set; }
        [Required(ErrorMessage = "Цена за метр обязательна для заполнения")]
        [Display(Name = "Цена за метр")]
        public Core.AnchorCalculator.Entities.Enums.Type Type { get; set; }
        public double PricePerMetr { get; set; }

        public Array? Types { get; set; }
        public string[]? Names { get; set; }
    }
}
