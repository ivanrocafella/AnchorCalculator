using Core.AnchorCalculator.Entities;
using UI.AnchorCalculator.Extensions;

namespace UI.AnchorCalculator.ViewModels
{
    public class MaterialsAndCostWorkViewModel
    {
        public List<Material>? Materials { get; set; }
        public CostWork? CostWork { get; set; }
        public double MarginPercent { get { return CostWork.Margin * 100; } set { CostWork.Margin = value / 100; } }
    }
}
