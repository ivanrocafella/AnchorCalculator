using Core.AnchorCalculator.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace UI.AnchorCalculator.ViewModels
{
    public class FilterViewModelAnchors
    {
        public List<Material> Materials { get; private set; } // список материалов
        public int? SelectedMaterial { get; private set; }   // выбранный материал
        public string SelectedUserName { get; private set; }    // введенный логин автора
        public DateTime DateTimeFrom { get; private set; }    // дата создания от 
        public DateTime DateTimeTill { get; private set; }    // дата создания до 
        public double PriceFrom { get; private set; }    // цена от 
        public double PriceTill { get; private set; }    // цена до 

        public FilterViewModelAnchors(List<Material> materials, int? selectedMaterial, string selectedUserName
            , DateTime dateTimeFrom, DateTime dateTimeTill, double priceFrom, double priceTill)
        {
            Materials = materials;
            SelectedMaterial = selectedMaterial;
            SelectedUserName = selectedUserName;
            DateTimeFrom = dateTimeFrom;
            DateTimeTill = dateTimeTill;
            PriceFrom = priceFrom;
            PriceTill = priceTill;
        }
    }
}
