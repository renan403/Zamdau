using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamdau.Interfaces;
using Zamdau.Models;

namespace Zamdau.Controllers
{
    [Authorize]
    public class SellerController(IUserService user, ISellerService seller) : Controller
    {
        private IUserService _user = user;
        private ISellerService _seller = seller;

        public async Task<IActionResult> SellerProfile()
        {
            var seller = await _seller.GetSellerAsync(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
           //var seller = new Seller()
           //{
           //    Name = "John Doe",
           //    Description = "An experienced artisan specializing in handmade goods and unique crafts.",
           //    Email = "johndoe@example.com",
           //    Phone = "+1 (555) 123-4567",
           //    ProfilePictureUrl = "https://via.placeholder.com/200",
           //    Products = new List<ProductViewModel>
           //{
           //    new ProductViewModel
           //    {
           //        Id = 1,
           //        Name = "Handmade Ceramic Vase",
           //        Price = 49.99m,
           //        ImageUrl = "https://via.placeholder.com/300x200"
           //    },
           //    new ProductViewModel
           //    {
           //        Id = 2,
           //        Name = "Wooden Cutting Board",
           //        Price = 29.99m,
           //        ImageUrl = "https://via.placeholder.com/300x200"
           //    },
           //    new ProductViewModel
           //    {
           //        Id = 3,
           //        Name = "Knitted Scarf",
           //        Price = 19.99m,
           //        ImageUrl = "https://via.placeholder.com/300x200"
           //    },
           //
           //}
           //};

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
            //var seller = new Seller()
            //{
            //    Name = "John Doe",
            //    Description = "An experienced artisan specializing in handmade goods and unique crafts.",
            //    Email = "johndoe@example.com",
            //    Phone = "+1 (555) 123-4567",
            //    ProfilePictureUrl = "https://via.placeholder.com/200",
            //    Products = new List<ProductViewModel>
            //{
            //    new ProductViewModel
            //    {
            //        Id = 1,
            //        Name = "Handmade Ceramic Vase",
            //        Price = 49.99m,
            //        ImageUrl = "https://via.placeholder.com/300x200"
            //    },
            //
            //
            //}
            //};

            return View(seller);


        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateSeller model, IFormFile ProfilePicture)
        {
            if (ModelState.IsValid)
            {
                model.teste = ProfilePicture;
                var seller = await _seller.UpdateSellerAsync(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value, model,ProfilePicture );
                return View(model);
            }
            return RedirectToAction("Profile", new { id = "model.Id" });
        }



        /*[HttpPost]
public IActionResult CreateProduct(Product model, IFormFile ImageUrl)
{
    if (ModelState.IsValid)
    {
        // Salvar imagem, se fornecida.
        if (ImageUrl != null && ImageUrl.Length > 0)
        {
            var fileName = Path.GetFileName(ImageUrl.FileName);
            var filePath = Path.Combine("wwwroot/uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                ImageUrl.CopyTo(stream);
            }
            model.ImageUrl = "/uploads/" + fileName;
        }

        // Salvar produto no banco de dados (não implementado aqui).
        // _context.Products.Add(model);
        // _context.SaveChanges();

        return RedirectToAction("Index");
    }
    return View(model);
}
*/
    }
}
