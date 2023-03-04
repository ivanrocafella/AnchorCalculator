using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ViewModels
{
    public class MaterialViewModel
    {
        [Display(Name = "Название")]
        public string? Name { get; set; }
        [Range(6, Double.MaxValue, ErrorMessage = "Размер должен быть не меньше 6")]
        [Display(Name = "Размер")]
        public double Size { get; set; }
        [Display(Name = "Тип сечения")]
        public int TypeId { get; set; }
        [Range(0.1, Double.MaxValue, ErrorMessage = "Укажите цену за метр")]
        [Display(Name = "Цена за метр")]
        public double PricePerMetr { get; set; }

        public Array? Types { get; set; }
        public string[]? Names { get; set; }
    }
}
