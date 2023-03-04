using Core.AnchorCalculator;
using Core.AnchorCalculator.Entities;
using DAL.AnchorCalculator;
using Microsoft.EntityFrameworkCore;
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
        public async Task AddMaterial(MaterialViewModel viewModel)
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
        }

        //Method for editing existing Material in the database
        public async Task EditMaterial(MaterialViewModelForEdit viewModel)
        {
            Material material = await GetMaterialById(viewModel.Id);
            material.TypeId = viewModel.TypeId;
            material.Name = viewModel.Name;
            material.Size = viewModel.Size;
            material.PricePerMetr = viewModel.PricePerMetr;
            applicationDbContext.Materials.Update(material);
            await applicationDbContext.SaveChangesAsync();
        }

        //Method for getting MaterialViewModel
        public MaterialViewModel GetMaterialViewModel()
        {
            MaterialViewModel materialViewModel = new()
            {
                Types = Enum.GetValues(typeof(Core.AnchorCalculator.Entities.Enums.Type)),
                Names = new string[] { "Арматура", "Круг", "Катанка" }
            };
            return materialViewModel;
        }

        //Method for getting all Materials 
        public async Task<List<Material>> GetAllMaterials() => await applicationDbContext.Materials.ToListAsync();

        //Method for getting 1 Material by id
        public async Task<Material> GetMaterialById(int id) => await applicationDbContext.Materials.FindAsync(id);

        //Method for getting MaterialViewModelForEdit
        public async Task<MaterialViewModelForEdit> GetMaterialViewModelForEdit(int id)
        {
            Material material = await GetMaterialById(id);
            MaterialViewModel materialViewModel = GetMaterialViewModel();
            MaterialViewModelForEdit viewModelForEdit = new MaterialViewModelForEdit()
            {
                Id = material.Id,
                Name = material.Name,
                Size = material.Size,
                TypeId = material.TypeId,
                PricePerMetr = material.PricePerMetr,
                Types = materialViewModel.Types,
                Names = materialViewModel.Names.Where(e => e != material.Name).ToArray(),
                Type = material.Type
            };
            return viewModelForEdit;
        }

        //Method for delete 1 Material by id
        public async Task DeleteById(int id)
        {
            Material material = await GetMaterialById(id);
            applicationDbContext.Remove(material);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
