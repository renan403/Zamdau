using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Zamdau.Interfaces;
using Zamdau.Models;
using Zamdau.Services;

namespace Zamdau.Controllers
{
    public class BuyerController(IUserService iUser) : Controller
    {
        private readonly IUserService _user = iUser;

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();

        }

        [HttpGet]
        public IActionResult SignUp() => View();
        [HttpGet]
        public IActionResult PasswordReset() => View(new ResetPWD());
        [HttpGet]
        public IActionResult PasswordSent() => View();
        [HttpGet]
        public IActionResult ValidateYourEmail() => View();
        [HttpGet]
        public IActionResult ReSendValidateInEmail(string email)
        {
            _user.ReSendVerificationEmail(HttpContext.Session.GetString("tokenClientResend"));
            return RedirectToAction("ValidateYourEmail", "User");
        }

        public async Task<IActionResult> SingOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Login(SignIn userLogin)
        {
            
            if (ModelState.IsValid)
            {
                
                var response = await _user.Login(userLogin.Email, Helpers.Encrypt(userLogin.Password));
                if (!string.IsNullOrEmpty(response.Error))
                {
                    ViewBag.Error = response.Error.Replace("_", " ");
                    return View();
                }
                if (response.IsAuthenticated)
                {
                    var user = await _user.GetUser(response.TokenID.ToString());
                    var claims = new List<Claim>
                     {
                        new("UserId", response.TokenID.ToString()),
                        new (ClaimTypes.Name, user.Name),
                        new (ClaimTypes.Email, user.Email),
                     };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    if (TempData["ReturnUrl"] != null)
                    {
                        string returnUrl = TempData["ReturnUrl"].ToString();
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);

                    }

                    return RedirectToAction("Index", "Home");
                }
                ViewBag.IsAutenticatedError = "Your access is not authenticated, please confirm in your email.";

            }
            return View(userLogin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUp signUp, IFormCollection form)
        {

            if (ModelState.IsValid)
            {
                var captchaVerification = await ValidationRecaptcha(form);
                if (captchaVerification.Success)
                {
                    var response = await _user.RegisterAccount(new SignUp()
                    {
                        Name = signUp.Name.ToLower(),
                        Pwd = Helpers.Encrypt(signUp.Pwd),
                        Email = signUp.Email,
                        CreatedAt = DateTime.UtcNow,
                        Age = signUp.Age,
                    });
                    if (string.IsNullOrEmpty(response))
                        return RedirectToAction("Error");
                    return RedirectToAction("ValidateYourEmail", "Buyer");
                }
                else // reCAPTCHA falhou
                {
                    var errors = string.Join(", ", captchaVerification.ErrorCodes);
                    errors = errors.Replace("invalid-input-response", "Invalid CAPTCHA");
                    ViewBag.RecaptchaError = errors;
                    return View("SignUp", signUp);
                }
            }
            return View(signUp);
        }



        [HttpPost]
        public async Task<IActionResult> PasswordReset(IFormCollection form)
        {
            var captchaVerification = await ValidationRecaptcha(form);

            if (captchaVerification.Success)
            {
                await _user.ResetPasswordAccess(form["Email"]);
                return RedirectToAction("PasswordSent");
            }
            else // reCAPTCHA falhou
            {
                var errors = string.Join(", ", captchaVerification.ErrorCodes);
                errors = errors.Replace("invalid-input-response", "Invalid CAPTCHA");
                return View("PasswordReset", new ResetPWD() { Error = errors });
            }
        }

        public async Task<CaptchaResponse?> ValidationRecaptcha(IFormCollection form)
        {
            var recaptchaResponse = form["g-recaptcha-response"];
            var client = new HttpClient();
            var result = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={"6LedYBoqAAAAAPfQH1WObVJwBsTd1KWq_lWiH1jI"}&response={recaptchaResponse}");

            return JsonConvert.DeserializeObject<CaptchaResponse>(result);

        }
        public class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public string[] ErrorCodes { get; set; }

            [JsonProperty("challenge_ts")]
            public DateTime challenge_ts { get; set; }
        }
    }
}
