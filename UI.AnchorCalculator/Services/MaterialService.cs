using Core.AnchorCalculator;
using Core.AnchorCalculator.Entities;
using DAL.AnchorCalculator;
using UI.AnchorCalculator.ViewModels;

namespace UI.AnchorCalculator.Services
{
    public class MaterialService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public MaterialService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }





        //Method for adding new Material to the database
        public async Task<Material> AddMaterial(MaterialViewModel viewModel)
        {
            Material material = new()
            {
                Name = viewModel.Name,
                Size = viewModel.Size,
                TypeId = viewModel.TypeId,
                PricePerMetr = viewModel.PricePerMetr
            };
            await applicationDbContext.Materials.AddAsync(material);
            await applicationDbContext.SaveChangesAsync();
            return material;
        }
    }
}
