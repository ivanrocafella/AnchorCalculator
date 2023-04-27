using Core.AnchorCalculator;
using Core.AnchorCalculator.Entities;
using DAL.AnchorCalculator;
using Microsoft.EntityFrameworkCore;
using System.IO;
using UI.AnchorCalculator.Extensions;
using UI.AnchorCalculator.ViewModels;

namespace UI.AnchorCalculator.Services
{
    public class MaterialService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment environment;
        private readonly LoggerManager _logger;
        public MaterialService(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment, LoggerManager logger)
        {
            _applicationDbContext = applicationDbContext;
            this.environment = environment;
            _logger = logger;
        }

        //Method for adding new Material to the database
        public async Task AddMaterial(MaterialViewModel viewModel)
        {
            Material material = new()
            {
                Name = viewModel.Name,
                Size = viewModel.Size,
                TypeId = viewModel.TypeId,
                PricePerMetr = viewModel.PricePerMetr,
                DateUpdate = DateTime.UtcNow,
                TimeTheradRolling = viewModel.TimeTheradRolling,
                TimeBandSaw = viewModel.TimeBandSaw,
                LengthBladeBandSaw = viewModel.LengthBladeBandSaw
        };
            await _applicationDbContext.Materials.AddAsync(material);
            await _applicationDbContext.SaveChangesAsync();
        }

        //Method for editing existing Material in the database
        public async Task EditMaterial(MaterialViewModelForEdit viewModel)
        {
            Material material = _applicationDbContext.Materials.Find(viewModel.Id);
            material.TypeId = viewModel.TypeId;
            material.Name = viewModel.Name;
            material.Size = viewModel.Size;
            material.PricePerMetr = viewModel.PricePerMetr;
            material.DateUpdate = DateTime.UtcNow;
            material.TimeTheradRolling = viewModel.TimeTheradRolling;
            material.TimeBandSaw = viewModel.TimeBandSaw;
            material.LengthBladeBandSaw = viewModel.LengthBladeBandSaw;
            _applicationDbContext.Materials.Update(material);
            await _applicationDbContext.SaveChangesAsync();
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

        //Method for getting MaterialsAndCostWorkViewModel
        public async Task<MaterialsAndCostWorkViewModel> GetMaterialsAndCostWorkViewModel()
        {
            CostWork costWork = new();
            MaterialsAndCostWorkViewModel materialsAndCostWorkViewModel = new()
            {
                Materials = await _applicationDbContext.Materials.OrderBy(x => x.Name).ThenBy(x => x.Size).ToListAsync(),
            };
            try
            {
                materialsAndCostWorkViewModel.CostWork = await costWork.GetCostWork(environment);
            }
            catch (Exception ex)
            {
                string exception = $"Error:{ex.Message}";
                _logger.LogDebug(exception);
                throw;
            }
            return materialsAndCostWorkViewModel;
        }

        //Method for getting all Materials 
        public async Task<List<Material>> GetAllMaterials() => await _applicationDbContext.Materials.ToListAsync();

        //Method for getting material by id
        public async Task<Material> GetMaterialById(int id) => await _applicationDbContext.Materials.FindAsync(id);

        //Method for getting MaterialViewModelForEdit
        public MaterialViewModelForEdit GetMaterialViewModelForEdit(int id)
        {
            Material material = _applicationDbContext.Materials.Find(id);
            MaterialViewModel materialViewModel = GetMaterialViewModel();
            MaterialViewModelForEdit viewModelForEdit = new()
            {
                Id = material.Id,
                Name = material.Name,
                Size = material.Size,
                TypeId = material.TypeId,
                PricePerMetr = material.PricePerMetr,
                Types = materialViewModel.Types,
                Names = materialViewModel.Names.Where(e => e != material.Name).ToArray(),
                Type = material.Type,
                TimeTheradRolling = material.TimeTheradRolling,
                TimeBandSaw = material.TimeBandSaw,
                LengthBladeBandSaw = material.LengthBladeBandSaw
            };
            return viewModelForEdit;
        }

        //Method for delete 1 Material by id
        public async Task DeleteById(int id)
        {
            Material material = _applicationDbContext.Materials.Find(id);
            _applicationDbContext.Remove(material);
            await _applicationDbContext.SaveChangesAsync();
        }

        //Method for edit CostWork
        public async Task EditCostWork(MaterialsAndCostWorkViewModel materialsAndCostWorkViewModel)
        {
            CostWork costWork = new()
            {
                ExchangeDollar = materialsAndCostWorkViewModel.CostWork.ExchangeDollar,
                AreaWelding = materialsAndCostWorkViewModel.CostWork.AreaWelding,
                LengthEffective = materialsAndCostWorkViewModel.CostWork.LengthEffective,
                PriceBandSaw = materialsAndCostWorkViewModel.CostWork.PriceBandSaw,
                TimeSetTheradRolling = materialsAndCostWorkViewModel.CostWork.TimeSetTheradRolling,
                TimeBend = materialsAndCostWorkViewModel.CostWork.TimeBend,
                TimeSetBend = materialsAndCostWorkViewModel.CostWork.TimeSetBend,
                Margin = materialsAndCostWorkViewModel.CostWork.Margin
            };
            await costWork.AddCostWork(costWork,environment);
        }
    }
}
