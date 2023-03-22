using Core.AnchorCalculator.Entities;
using Core.AnchorCalculator.Entities.Enums;
using GrapeCity.Documents.Svg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI.AnchorCalculator.Extensions;
using UI.AnchorCalculator.Services;
using UI.AnchorCalculator.ViewModels;

namespace UI.AnchorCalculator.Controllers
{
    public class AnchorController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly AnchorService _AService;
        private readonly SvgMakingService _SvgService;
        private readonly CalculateService _CService;
        private readonly UserManager<User> _userManager;

        public AnchorController(IWebHostEnvironment appEnvironment, AnchorService aService, SvgMakingService svgService, CalculateService cService, UserManager<User> userManager)
        {
            _appEnvironment = appEnvironment;
            _AService = aService;
            _SvgService = svgService;
            _CService = cService;
            _userManager = userManager;
        }

        // GET: AnchorController
        public ActionResult Index()
        {
            AnchorViewModel viewModel = _AService.GetAnchorViewModel();
            return View(viewModel);
        }

        // GET: AnchorController
        public async Task<ActionResult> Anchors(string? SelectedMaterial, string SelectedUserName
            , DateTime DateTimeFrom, DateTime DateTimeTill, double PriceFrom, double PriceTill, int PageSize = 6, int page = 1)
        {
            IQueryable<Anchor> anchors = _AService.GetAll(); 
            _AService.Filter(ref anchors, SelectedMaterial, SelectedUserName, DateTimeFrom, DateTimeTill, PriceFrom, PriceTill); // filter
            PagingData pagingData = _AService.Pagination(ref anchors, PageSize, page);
            AnchorsViewModel anchorsViewModel = await _AService.GetAnchorsViewModel(anchors, SelectedMaterial, SelectedUserName, DateTimeFrom
                , DateTimeTill, PriceFrom, PriceTill, pagingData);
            return View(anchorsViewModel);
        }

        // GET: AnchorController
        public async Task<JsonResult> GetListAnchorJsonResult(string ids)
        {
            List<Anchor> anchors = await _AService.GetListAnchorFromPage(ids);
            var anchorsSvg = anchors.Select(e => e.SvgElement).ToList();
            var anchorsId = anchors.Select(e => e.Id).ToList();
            if (anchorsSvg.Count>0)
                return Json(new { success = true, anchorsSvg = anchorsSvg, idMin = anchorsId[0], idMax = anchorsId[^1]});
            else
                return Json(new { success = false });
        }

        // GET: AnchorController
        [HttpGet]
        public async Task<JsonResult> GetAnchorJsonResult(int id)
        {
            Anchor anchor = await _AService.GetAnchorById(id);
            if (anchor != null)
                return Json(new { success = true, svgElement = anchor.SvgElement });
            else
                return Json(new { success = false });
        }

        // GET: AnchorController

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> GetAnchorJsonResult(AnchorViewModel viewModel)
        {
            if ((viewModel.Kind == Kind.Bend.ToString() && viewModel.BendLength >= 0 && viewModel.BendLength < 100) || viewModel.BendLength > 500)
            {
                ModelState.AddModelError(nameof(viewModel.BendLength), "Длина загиба должна быть от 100 до 500");
            }
            if ((viewModel.Kind == Kind.BendDouble.ToString() && viewModel.BendLength >= 0 && viewModel.BendLength < 200) || viewModel.BendLength > 500)
            {
                ModelState.AddModelError(nameof(viewModel.BendLength), "Длина загиба должна быть от 200 до 500");
            }
            if (viewModel.ThreadDiameter == 0)
            {
                ModelState.AddModelError(nameof(viewModel.ThreadDiameter), "Диаметр резьбы не может быть равен 0");
            }
            if (viewModel.ThreadDiameter > viewModel.Diameter)
            {
                ModelState.AddModelError(nameof(viewModel.ThreadDiameter), "Диаметр резьбы должен быть меньше или равен диаметру анкера");
            }
            if (ModelState.IsValid)
            {
                Anchor Anchor = await _AService.GetAnchor(viewModel);
                if (Anchor.Kind == Kind.Straight)
                    _SvgService.GetSvgStraightAnchor(Anchor);
                if (Anchor.Kind == Kind.Bend)
                    _SvgService.GetSvgBendAnchor(Anchor);
                if (Anchor.Kind == Kind.BendDouble)
                    _SvgService.GetSvgBendDoubleAnchor(Anchor);
                //_SvgService.GetSvg(Anchor);
                await _CService.Calculate(Anchor);
                if (User.Identity.IsAuthenticated)
                    return Json(new { success = true, anchorJS = Anchor, isAuthen = true });
                else
                    return Json(new { success = true, anchorJS = Anchor, isAuthen = false });
            }
            else
            {
                if (ModelState.Root.Children[9].Errors.Count > 0 && ModelState.Root.Children[4].Errors.Count > 0)
                    return Json(new { success = false
                        , errorMessageDiam = ModelState.Root.Children[9].Errors[0].ErrorMessage
                        , errorMessageBendLen = ModelState.Root.Children[4].Errors[0].ErrorMessage
                    });
                else if (ModelState.Root.Children[9].Errors.Count > 0)
                {
                    return Json(new { success = false, errorMessageDiam = ModelState.Root.Children[9].Errors[0].ErrorMessage });
                }
                else if (ModelState.Root.Children[4].Errors.Count > 0)
                {
                    return Json(new { success = false, errorMessageBendLen = ModelState.Root.Children[4].Errors[0].ErrorMessage });
                }
                else
                    return Json(new { succes = false });
            }    
        }

        // GET: AnchorController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id != 0)
            {
                Anchor anchor = await _AService.GetAnchorById(id);
                AnchorViewModel viewModel = _AService.GetAnchorViewModelForDetails(anchor);
                return View(viewModel);
            }
            else
                return NoContent();
        }

        // GET: AnchorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnchorController/Add
        [HttpPost]
        public async Task<ActionResult> Add(AnchorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await CurrentUser.Get(_userManager, User.Identity.Name);
                await _AService.AddAnchor(viewModel, user.Id);
                return Json(new { success = true });
            }
            else
                return Json(new { success = false });           
        }

        // GET: AnchorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnchorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnchorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnchorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
