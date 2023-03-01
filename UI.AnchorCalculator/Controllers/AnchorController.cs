using Core.AnchorCalculator.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.AnchorCalculator.Services;
using UI.AnchorCalculator.ViewModels;

namespace UI.AnchorCalculator.Controllers
{
    public class AnchorController : Controller
    {
        private readonly AnchorService _AService;

        public AnchorController(AnchorService aService)
        {
            _AService = aService;
        }


        // GET: AnchorController
        public ActionResult Index()
        {
            AnchorViewModel viewModel = _AService.GetAnchorViewModel();
            return View(viewModel);
        }


        // GET: AnchorController

        [HttpGet]
        public JsonResult GetAnchorJsonResult(AnchorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Anchor Anchor = _AService.GetAnchor(viewModel);
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
