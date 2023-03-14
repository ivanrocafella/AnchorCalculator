using DAL.AnchorCalculator;
using Microsoft.AspNetCore.Mvc;

namespace UI.AnchorCalculator.Controllers
{
    public class ValidationController : Controller
    {
        private ApplicationDbContext _context;

        public ValidationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AcceptVerbs("GET", "POST")]
        public bool CheckExistAccount(string word) => !_context.Users.Any(e => e.UserName == word || e.Email == word);

    }
}
