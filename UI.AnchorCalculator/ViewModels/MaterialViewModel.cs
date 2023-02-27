using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ViewModels
{
    public class MaterialViewModel
    {
        [Required(ErrorMessage = "Название обязательно для заполнения")]
        [Display(Name = "Название")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Размер обязателен для заполнения")]
        [Display(Name = "Размер")]
        public int Size { get; set; }
        [Required(ErrorMessage = "Тип сечения обязателен для заполнения")]
        [Display(Name = "Тип сечения")]
        public int TypeId { get; set; }
        [Required(ErrorMessage = "Цена за метр обязательна для заполнения")]
        [Display(Name = "Цена за метр")]
        public double PricePerMetr { get; set; }
        public Array Types { get; set; }    
    }
}
