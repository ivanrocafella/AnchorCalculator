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
            if ((viewModel.BendLength > 0 && viewModel.BendLength < 100) || viewModel.BendLength > 500)
            {
                ModelState.AddModelError(nameof(viewModel.BendLength), "Длина загиба должна быть от 100 до 500");
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
                Anchor Anchor = _AService.GetAnchor(viewModel);
                _SvgService.GetSvg(Anchor, _appEnvironment.WebRootPath);
                _CService.Calculate(Anchor);
                return Json(new { success = true, anchorJS = Anchor });
            }
            else
            {
                if (ModelState.Root.Children[8].Errors.Count > 0 && ModelState.Root.Children[3].Errors.Count > 0)
                    return Json(new { success = false
                        , errorMessageDiam = ModelState.Root.Children[8].Errors[0].ErrorMessage
                        , errorMessageBendLen = ModelState.Root.Children[3].Errors[0].ErrorMessage
                    });
                else if (ModelState.Root.Children[8].Errors.Count > 0)
                {
                    return Json(new { success = false, errorMessageDiam = ModelState.Root.Children[8].Errors[0].ErrorMessage });
                }
                else if (ModelState.Root.Children[3].Errors.Count > 0)
                {
                    return Json(new { success = false, errorMessageBendLen = ModelState.Root.Children[3].Errors[0].ErrorMessage });
                }
                else
                    return Json(new { succes = false });
            }    
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

        // POST: AnchorController/Add
        [HttpPost]
        public async Task<ActionResult> Add(AnchorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _AService.AddAnchor(viewModel);
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

        // Validation 

      //  [AcceptVerbs("GET", "POST")]
      //  public bool CheckThreadLength(string ThreadLength) => int.Parse(ThreadLength) >= 50 && int.Parse(ThreadLength) <= 100;
      //
      //  [AcceptVerbs("GET", "POST")]
      //  public bool CheckBendLength(string BendLength) => int.Parse(BendLength) >= 100 && int.Parse(BendLength) <= 500;
    }
}
