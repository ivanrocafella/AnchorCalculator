using Core.AnchorCalculator.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI.AnchorCalculator.ViewModels
{
    public class FilterViewModelAnchors
    {
        public List<Material> Materials { get; private set; } // список материалов
        public int? SelectedMaterial { get; private set; }   // выбранный материал
        public string SelectedUserName { get; private set; }    // введенный логин автора
        public FilterViewModelAnchors(List<Material> materials, int? material, string userName)
        {
            Materials = materials;
            SelectedMaterial = material;
            SelectedUserName = userName;
        }
    }
}
