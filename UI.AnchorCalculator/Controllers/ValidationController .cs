using DAL.AnchorCalculator;
using Microsoft.AspNetCore.Mvc;

namespace UI.AnchorCalculator.Controllers
{
    public class ValidationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ValidationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AcceptVerbs("GET", "POST")]
        public bool CheckExistAccountByEmail(string Email) => !_context.Users.Any(e => e.Email == Email);
        [AcceptVerbs("GET", "POST")]
        public bool CheckExistAccountByUserName(string UserName) => !_context.Users.Any(e => e.UserName == UserName);

    }
}
