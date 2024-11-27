using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Zamdau.Models;
using System.Diagnostics;
using Zamdau.Interfaces;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Zamdau.Controllers
{
    [Authorize]
    public class UserController(IUserService user) : Controller
    {
        private readonly IUserService _user = user;
        [HttpGet]
        public async Task<IActionResult> Account() => View(await _user.GetUser(User.Claims.FirstOrDefault( u => u.Type == "UserId")?.Value));
        [HttpGet]
        public IActionResult Cart() => View();
        [HttpGet]
        public IActionResult Address() => View();
        [HttpGet]
        public IActionResult AddAddress() => View();
        
        [HttpGet]
        public async Task<IActionResult> MyAccount()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var user = await _user.GetUser(userId);

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> MyAccount(Account model)
        {
            if (ModelState.IsValid)
            {
                var id = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                if (id != null)
                    if (await _user.SavePartialUserAsync(id, model))
                        return RedirectToAction("MyAccount");
            }
            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }


}
