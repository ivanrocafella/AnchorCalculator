using Core.AnchorCalculator.Entities;
using GrapeCity.Documents.Svg;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AnchorController(IWebHostEnvironment appEnvironment, AnchorService aService, SvgMakingService svgService, CalculateService cService)
        {
            _appEnvironment = appEnvironment;
            _AService = aService;
            _SvgService = svgService;
            _CService = cService;
        }

        // GET: AnchorController
        public ActionResult Index()
        {
            AnchorViewModel viewModel = _AService.GetAnchorViewModel();
            return View(viewModel);
        }


        // GET: AnchorController

        [HttpPost]
        public JsonResult GetAnchorJsonResult(AnchorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Anchor Anchor = _AService.GetAnchor(viewModel);
                _SvgService.GetSvg(Anchor, _appEnvironment.WebRootPath);
                _CService.Calculate(Anchor);
                return Json(new { success = true, anchorJS = Anchor });
            }
            return Json(new { succes = false });
        }


        // GET: AnchorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AnchorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnchorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
