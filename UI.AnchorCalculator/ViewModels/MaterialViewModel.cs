using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ViewModels
{
    public class MaterialViewModel
    {
        [Display(Name = "Название")]
        public string? Name { get; set; }
        [Range(6, Double.MaxValue, ErrorMessage = "Размер должен быть не меньше 6")]
        [Display(Name = "Размер,мм")]
        public double Size { get; set; }
        [Display(Name = "Тип сечения")]
        public int TypeId { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "Укажите цену за метр")]
        [Display(Name = "Цена за метр,сом")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public double PricePerMetr { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "Укажите время накатки резьбы")]
        [Display(Name = "Время накатки резьбы,ч")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public double TimeThreadRolling { get; set; } // unity of measure = н
        [Range(0, Double.MaxValue, ErrorMessage = "Укажите время нарезки резьбы")]
        [Display(Name = "Время нарезки резьбы,ч")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public double TimeThreadCutting { get; set; } // unity of measure = н
        [Range(0, Double.MaxValue, ErrorMessage = "Укажите кол-во плашки")]
        [Display(Name = "Кол-во плашки")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public double Plashka { get; set; } // unity of measure = 1 плашка
        [Range(0, Double.MaxValue, ErrorMessage = "Укажите кол-во резца")]
        [Display(Name = "Кол-во резца")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public double Cutter { get; set; } // unity of measure = 1 резец
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
