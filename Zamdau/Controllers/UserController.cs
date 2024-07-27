using Microsoft.AspNetCore.Mvc;

namespace Rcsp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string teste)
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
