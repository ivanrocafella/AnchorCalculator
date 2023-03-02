using Core.AnchorCalculator.Entities;
using DAL.AnchorCalculator;
using UI.AnchorCalculator.ViewModels;

namespace UI.AnchorCalculator.Services
{
    public class AnchorService
    {
        private readonly MaterialService MService;
        private readonly ApplicationDbContext applicationDbContext;

        public AnchorService(MaterialService mService, ApplicationDbContext applicationDbContext)
        {
            MService = mService;
            this.applicationDbContext = applicationDbContext;
        }

        //Method for getting AnchorViewModel
        public AnchorViewModel GetAnchorViewModel()
        {
            AnchorViewModel anchorViewModel = new AnchorViewModel()
            {
                Materials = MService.GetAllMaterials().Result
            };    
            return anchorViewModel;
        }

        //Method for getting Anchor
        public Anchor GetAnchor(AnchorViewModel viewModel)
        {
            Anchor anchor = new()
            {
                MaterialId = viewModel.MaterialId,
                Diameter = viewModel.Diameter,
                ThreadDiameter = viewModel.ThreadDiameter,
                Length = viewModel.Length,
                ThreadLength = viewModel.ThreadLength,
                BendLength = viewModel.BendLength,
                BendRadius = viewModel.BendRadius,
                ThreadStep = viewModel.ThreadStep,
                Quantity = viewModel.Quantity
            };
            return anchor;
        }
    }
}
