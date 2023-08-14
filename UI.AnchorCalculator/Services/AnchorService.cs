using Core.AnchorCalculator.Entities;
using Core.AnchorCalculator.Entities.Enums;
using DAL.AnchorCalculator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using UI.AnchorCalculator.Extensions;
using UI.AnchorCalculator.ViewModels;

namespace UI.AnchorCalculator.Services
{
    public class AnchorService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly MaterialService MService;
        private readonly UserManager<User> _userManager;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly LoggerManager _loggerManager;

        public AnchorService(ApplicationDbContext applicationDbContext, MaterialService mService, UserManager<User> userManager, LoggerManager loggerManager)
        {
            this.applicationDbContext = applicationDbContext;
            MService = mService;
            _userManager = userManager;
            _loggerManager = loggerManager;
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
            AnchorViewModel anchorViewModel = new()
            {
                Anchor = anchor
            };
            if(anchor.Material != null)
                anchorViewModel.MaterialName = anchor.Material.FullName;
            if (anchor.User != null)
                anchorViewModel.UserName = anchor.User.UserName;
            return anchorViewModel;
        }

        //Method for getting Anchor
        public async Task<Anchor> GetAnchor(AnchorViewModel viewModel)
        {
            logger.Debug($"viewModel.ThreadStep: {viewModel.ThreadStep}; culture: {Thread.CurrentThread.CurrentCulture.DisplayName}"); // logging of input ThreadStep and currentculture
            List<Kind> kinds = Enum.GetValues(typeof(Kind)).Cast<Kind>().ToList();
            var threadStep = float.Parse(viewModel.ThreadStep, CultureInfo.InvariantCulture);
            var diameter = float.Parse(viewModel.Diameter, CultureInfo.InvariantCulture);

            Anchor anchor = new()
            {
                MaterialId = viewModel.MaterialId,
                Diameter = diameter,
                ThreadDiameter = viewModel.ThreadDiameter,
                Length = viewModel.Length,
                ThreadLength = viewModel.ThreadLength,
                BendLength = viewModel.BendLength,
                BendRadius = viewModel.BendRadius,
                ThreadStep = threadStep,
                Quantity = viewModel.Quantity,
                Material = await MService.GetMaterialById(viewModel.MaterialId),
                KindId = (int)kinds.FirstOrDefault(e => e.ToString() == viewModel.Kind),
                ThreadLengthSecond = viewModel.ThreadLengthSecond,
                ProductionId = viewModel.ThreadLength > 100 || viewModel.ThreadLengthSecond > 100
                ? (int)Production.CuttingThread 
                : (int)Production.RollingThread,
            };
            return anchor;
        }

        //Method for getting Anchor by id
        public async Task<Anchor> GetAnchorById(int id)
        { 
            Anchor anchor = await applicationDbContext.Anchors.FindAsync(id);
            if (!String.IsNullOrEmpty(anchor.MaterialJson))
                anchor.Material = JsonSerializer.Deserialize<Material>(anchor.MaterialJson);
            if (!String.IsNullOrEmpty(anchor.UserJson))
                anchor.User = JsonSerializer.Deserialize<User>(anchor.UserJson);
            return anchor;
        }

        //Method for getting All anchors
        public IQueryable<Anchor> GetAll()
        {
            IQueryable<Anchor> anchors = applicationDbContext.Anchors.OrderBy(x => x.Id);
            foreach (var item in anchors)
            {
                if (!String.IsNullOrEmpty(item.MaterialJson))
                    item.Material = JsonSerializer.Deserialize<Material>(item.MaterialJson);
                if (!String.IsNullOrEmpty(item.UserJson))
                    item.User = JsonSerializer.Deserialize<User>(item.UserJson);
            }
            return anchors;
        }

        //Method for getting list anchors from page
        public async Task<List<Anchor>> GetListAnchorFromPage(string ids)
        {
            List<Anchor> anchors = new();
            if (!String.IsNullOrEmpty(ids))
            {
                string[] strIds = ids.Split(new char[] { ',' });
                int[] intIds = Array.ConvertAll(strIds, e => int.Parse(e));
                foreach (var item in intIds)
                    anchors.Add(await GetAnchorById(item));
            }
            else
                anchors = GetAll().ToList();
            return anchors;
        }

        //Method for filtration anchors
        public void Filter(ref IQueryable<Anchor> anchors, string? SelectedMaterial, string SelectedUserName
            , DateTime DateTimeFrom, DateTime DateTimeTill, double PriceFrom, double PriceTill)
        {
            if (!String.IsNullOrEmpty(SelectedMaterial))
                anchors = anchors.Where(e => e.MaterialJson.Contains(SelectedMaterial));
            if (!String.IsNullOrEmpty(SelectedUserName))
                anchors = anchors.Where(e => e.User.UserName.Contains(SelectedUserName));
            if (DateTimeFrom > DateTime.MinValue && DateTimeFrom < DateTime.MaxValue && DateTimeTill <= DateTime.MinValue)
                anchors = anchors.Where(e => e.DateCreate >= DateTimeFrom);
            if (DateTimeTill > DateTime.MinValue && DateTimeTill < DateTime.MaxValue && DateTimeFrom <= DateTime.MinValue)
                anchors = anchors.Where(e => e.DateCreate <= DateTimeTill);
            if (DateTimeFrom > DateTime.MinValue && DateTimeFrom < DateTime.MaxValue && DateTimeTill > DateTime.MinValue && DateTimeTill < DateTime.MaxValue)
                anchors = anchors.Where(e => e.DateCreate >= DateTimeFrom && e.DateCreate <= DateTimeTill);
            if (PriceFrom > 0 && PriceFrom < Double.PositiveInfinity && PriceTill == 0)
                anchors = anchors.Where(e => e.Price >= PriceFrom);
            if (PriceTill > 0 && PriceTill < Double.PositiveInfinity && PriceFrom == 0)
                anchors = anchors.Where(e => e.Price <= PriceTill);
            if (PriceFrom > 0 && PriceFrom < Double.PositiveInfinity && PriceTill > 0 && PriceTill < Double.PositiveInfinity)
                anchors = anchors.Where(e => e.Price >= PriceFrom && e.Price <= PriceTill);
        }


        //Method for pagination anchors
        public PagingData Pagination(ref IQueryable<Anchor> anchors, int pageSize, int page)
        {
            var countAllAnchors = anchors.Count();
            anchors = anchors.Skip((page - 1) * pageSize).Take(pageSize);
            PagingData pagingData = new(page, countAllAnchors, pageSize);
            return pagingData;
        }


        //Method for getting AnchorsViewModel for Anchors
        public async Task<AnchorsViewModel> GetAnchorsViewModel(IQueryable<Anchor> anchors, string? SelectedMaterial
            , string SelectedUserName, DateTime DateTimeFrom, DateTime DateTimeTill, double PriceFrom, double PriceTill, PagingData pagingData)
        {
            AnchorsViewModel anchorsViewModel = new() 
            {
                Anchors = anchors.ToList(),
                FilterView = new FilterViewModelAnchors(await MService.GetAllMaterials(), SelectedMaterial, SelectedUserName, DateTimeFrom, DateTimeTill, PriceFrom, PriceTill),
                PageViewModelAnchors = new PageViewModelAnchors(pagingData.Count, pagingData.Page, pagingData.PageSize)
            };
            return anchorsViewModel;
        }

        //Method for adding new Anchor to the database
        public async Task<int> AddAnchor(AnchorViewModel viewModel, string userId)
        {
           var options = new JsonSerializerOptions
           {
               Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
           };
           User user = await _userManager.FindByIdAsync(userId); 
           Material material = await MService.GetMaterialById(viewModel.MaterialId);
           string materialJson = JsonSerializer.Serialize<Material>(material, options);
           string userJson = JsonSerializer.Serialize<User>(user, options);
        
            try
            {
                Anchor anchor = new()
                {
                    Length = viewModel.Length,
                    Diameter = float.Parse(viewModel.Diameter, CultureInfo.InvariantCulture),
                    Weight = double.Parse(viewModel.Weight, CultureInfo.InvariantCulture),
                    Price = double.Parse(viewModel.Price, CultureInfo.InvariantCulture),
                    BendLength = viewModel.BendLength,
                    BendRadius = viewModel.BendRadius,
                    ThreadLength = viewModel.ThreadLength,
                    ThreadLengthSecond = viewModel.ThreadLengthSecond,
                    ThreadDiameter = viewModel.ThreadDiameter,
                    ThreadStep = float.Parse(viewModel.ThreadStep, CultureInfo.InvariantCulture),
                    Amount = double.Parse(viewModel.Amount, CultureInfo.InvariantCulture),
                    Quantity = viewModel.Quantity,
                    DateCreate = DateTime.Now,
                    SvgElement = viewModel.SvgElement,
                    BatchWeight = double.Parse(viewModel.BatchWeight, CultureInfo.InvariantCulture),
                    BilletLength = double.Parse(viewModel.BilletLength, CultureInfo.InvariantCulture),
                    MaterialId = viewModel.MaterialId,
                    Sebes = double.Parse(viewModel.Sebes, CultureInfo.InvariantCulture),
                    BatchSebes = double.Parse(viewModel.BatchSebes, CultureInfo.InvariantCulture),
                    UserId = userId,
                    UserJson = userJson,
                    MaterialJson = materialJson,
                    KindId = int.Parse(viewModel.Kind),
                    PriceMaterial = viewModel.PriceMaterial,
                    BatchPriceMaterial = viewModel.BatchPriceMaterial,
                    LengthPathRoller = viewModel.LengthPathRoller,
                    LengthBeforeEndPathRoller = viewModel.LengthBeforeEndPathRoller,
                    ProductionId = viewModel.ProductionId
                };
                await applicationDbContext.Anchors.AddAsync(anchor);
                await applicationDbContext.SaveChangesAsync();
                return anchor.Id;
            }
            catch (Exception ex)
            {
                string exception = $"Error:{ex.Message}";
                _loggerManager.LogDebug(exception);
                throw;
            }
        }

        //Method for delete 1 Anchor by id
        public async Task DeleteById(int id)
        {
            Anchor anchor = applicationDbContext.Anchors.Find(id);
            applicationDbContext.Remove(anchor);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
