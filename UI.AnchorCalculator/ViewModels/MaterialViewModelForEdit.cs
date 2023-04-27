using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ViewModels
{
    public class MaterialViewModelForEdit
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string? Name { get; set; }
        [Range(6, Double.MaxValue, ErrorMessage = "Размер должен быть не меньше 6")]
        [Display(Name = "Размер")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public double Size { get; set; }
        [Display(Name = "Тип сечения")]
        public int TypeId { get; set; }
        public Core.AnchorCalculator.Entities.Enums.Type Type { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "Укажите цену за метр")]
        [Display(Name = "Цена за метр")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public double PricePerMetr { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "Укажите время накатки резьбы")]
        [Display(Name = "Время накатки резьбы,ч")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public double TimeTheradRolling { get; set; } // unity of measure = н
        [Range(0, Double.MaxValue, ErrorMessage = "Укажите время лентопила")]
        [Display(Name = "Время лентопила,ч")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public double TimeBandSaw { get; set; } // unity of measure = н
        [Range(0, Double.MaxValue, ErrorMessage = "Укажите длину полотна лентопила")]
        [Display(Name = "Длина полотна лентопила,м")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public double LengthBladeBandSaw { get; set; } // unity of measure = мм

        public Array? Types { get; set; }
        public string[]? Names { get; set; }
    }
}
