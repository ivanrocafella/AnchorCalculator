using Core.AnchorCalculator.Entities;
using DAL.AnchorCalculator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.AnchorCalculator.ViewModels;

namespace UI.AnchorCalculator.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext applicationDbContext;

        public UsersController(UserManager<User> userManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Index() => View(_userManager.Users.ToList().Where(e => e.UserName != User.Identity.Name));

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new() { Email = model.Email, UserName = model.UserName};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                await applicationDbContext.SaveChangesAsync();
            }                                 
            return Ok();
        }
    }
}
