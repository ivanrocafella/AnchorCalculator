using Core.AnchorCalculator.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.AnchorCalculator.Services;
using UI.AnchorCalculator.ViewModels;

namespace UI.AnchorCalculator.Controllers
{
    public class MaterialController : Controller
    {
        private readonly MaterialService _MService;

        public MaterialController(MaterialService mService)
        {
            _MService = mService;
        }



        // GET: MaterialController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MaterialController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MaterialController/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: MaterialController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(MaterialViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Material material = _MService.AddMaterial(viewModel).Result;
                return Json(new { success = true, materialJS = material });
            }
            return Json(new { success = false });
        }   

        // GET: MaterialController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MaterialController/Edit/5
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

        // GET: MaterialController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MaterialController/Delete/5
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
