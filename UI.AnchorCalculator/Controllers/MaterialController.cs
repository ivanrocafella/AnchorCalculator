using Core.AnchorCalculator.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UI.AnchorCalculator.Extensions;
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
        public async Task<ActionResult> Index()
        {
            MaterialsAndCostWorkViewModel materialsAndCost = await _MService.GetMaterialsAndCostWorkViewModel();
            return View(materialsAndCost);
        }

        // GET: MaterialController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MaterialController/Add
        public ActionResult Add()
        {
            MaterialViewModel materialViewModel = _MService.GetMaterialViewModel();
            return View(materialViewModel);
        }

        // POST: MaterialController/Add
        [HttpPost]
        public async Task<ActionResult> Add(MaterialViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _MService.AddMaterial(viewModel);
                return RedirectToAction("Index");
            }
            else
                return View(viewModel);          
        }   

        // GET: MaterialController/Edit/5
        public ActionResult Edit(int id)
        {
            MaterialViewModelForEdit modelForEdit = _MService.GetMaterialViewModelForEdit(id);
            return View(modelForEdit);
        }

        // POST: MaterialController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MaterialViewModelForEdit modelForEdit)
        { 
            if (ModelState.IsValid)
            {
                await _MService.EditMaterial(modelForEdit);
                return RedirectToAction(nameof(Index));
            }
            else
                return View(modelForEdit);
        }

        // POST: MaterialController/Edit
        [HttpPost]
        public async Task<JsonResult> EditCostWork(MaterialsAndCostWorkViewModel materialsAndCost)
        {
            if (ModelState.IsValid)
            {
                await _MService.EditCostWork(materialsAndCost);
                return Json(new { success = true });
            }
            else
                return Json(new { success = false });
        }

        // POST: MaterialController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _MService.DeleteById(id);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        //Get material by Id
        [HttpGet]
        public async Task<JsonResult> GetMaterialJsonResult(int id)
        {
            Material material = new();
            if (id > 0)
                material = await _MService.GetMaterialById(id);                
            else
                material.Size = 0;
            return Json(new { success = true, materialJS = material });
        }
    }
}
