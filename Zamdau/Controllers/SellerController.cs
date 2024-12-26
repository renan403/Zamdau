using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamdau.Interfaces;
using Zamdau.Models;
using Zamdau.Services;


namespace Zamdau.Controllers
{
    [Authorize]
    public class SellerController(IUserService user, ISellerService seller) : Controller
    {
        private IUserService _user = user;
        private ISellerService _seller = seller;


        public async Task<IActionResult> SellerProfile(string? name)
        {
            
            Seller seller = name is not null ? await _seller.GetSellerByNameAsync(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value, name) : await _seller.GetSellerAsync(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);

            var session = await _user.GetEmailID(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
            if (session == seller.Uid)
                seller.OwnerUsername = true;
            return View(seller);
        }
        [HttpGet]
        public IActionResult BeSeller() => View();
        [HttpGet]
        public IActionResult RegisterSeller() => View();
        [HttpPost]
        public async Task<IActionResult> RegisterSeller(RegisterSeller seller)
        {
            await _seller.SaveSellerAsync(User.Claims.FirstOrDefault(u => u.Type == "UserId")?.Value, seller);

            return RedirectToAction("SellerProfile", "Seller");
        }
        [HttpGet]
        public IActionResult SellProduct() => View();
        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var seller = await _seller.GetSellerAsync(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
        
           
            return View(seller);


        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateSeller model, IFormFile? ProfilePictureUrl)
        {
            if (ModelState.IsValid)
            {
                var seller = await _seller.UpdateSellerAsync(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value, model, ProfilePictureUrl);
                return RedirectToAction("SellerProfile");
            }
            return RedirectToAction("Profile", new { id = "model.Id" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product,IFormFile ImageUrl )
        {
            var _return = await _seller.CreateProductAsync(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value, product, ImageUrl);
            if(_return)
                return RedirectToAction("SellerProfile");
            return View();
        }


    }
}
