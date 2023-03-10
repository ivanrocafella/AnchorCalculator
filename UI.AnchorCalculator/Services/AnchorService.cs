using Core.AnchorCalculator.Entities;
using DAL.AnchorCalculator;
using Microsoft.EntityFrameworkCore;
using UI.AnchorCalculator.ViewModels;

namespace UI.AnchorCalculator.Services
{
    public class AnchorService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly MaterialService MService;

        public AnchorService(ApplicationDbContext applicationDbContext, MaterialService mService)
        {
            this.applicationDbContext = applicationDbContext;
            MService = mService;
        }


        //Method for getting AnchorViewModel
        public AnchorViewModel GetAnchorViewModel()
        {
            AnchorViewModel anchorViewModel = new()
            {
                Materials = MService.GetAllMaterials().Result.OrderBy(x => x.Name).ThenBy(x => x.Size).ToList()
            };    
            return anchorViewModel;
        }

        //Method for getting AnchorViewModel for Details
        public AnchorViewModel GetAnchorViewModelForDetails(Anchor anchor)
        {
            Material material = applicationDbContext.Materials.Find(anchor.MaterialId);
            AnchorViewModel anchorViewModel = new()
            {
                Anchor = anchor,
                Material = $"{material.Name} Ø{material.Size} {material.Type}"
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
                Quantity = viewModel.Quantity,
                Material = MService.GetMaterialById(viewModel.MaterialId).Result
            };
            return anchor;
        }

        //Method for getting Anchor by id
        public Anchor GetAnchorById(int id) => applicationDbContext.Anchors.FindAsync(id).Result;

        //Method for getting All anchors
        public IQueryable<Anchor> GetAll() => applicationDbContext.Anchors.OrderBy(x => x.Id);

        //Method for adding new Anchor to the database
        public async Task AddAnchor(AnchorViewModel viewModel)
        {
           Anchor anchor = new()
           {
               Length = viewModel.Length,
               Diameter = viewModel.Diameter,
               Weight = viewModel.Weight,
               Price = viewModel.Price,
               BendLength = viewModel.BendLength,
               BendRadius = viewModel.BendRadius,
               ThreadLength = viewModel.ThreadLength,
               ThreadDiameter = viewModel.ThreadDiameter,
               ThreadStep = viewModel.ThreadStep,
               Amount = viewModel.Amount,
               Quantity = viewModel.Quantity,
               DateCreate = DateTime.Now,
               SvgElement = viewModel.SvgElement,
               BatchWeight = viewModel.BatchWeight,
               BilletLength = viewModel.BilletLength,
               MaterialId = viewModel.MaterialId,
               Sebes = viewModel.Sebes,
               BatchSebes = viewModel.BatchSebes
           };
           await applicationDbContext.Anchors.AddAsync(anchor);
           await applicationDbContext.SaveChangesAsync();
        }
    }
}
