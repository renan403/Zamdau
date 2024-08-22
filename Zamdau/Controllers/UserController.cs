using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Zamdau.Models;
using System.Diagnostics;
using Zamdau.Interfaces;

namespace Zamdau.Controllers
{
    public class UserController(IUser user) : Controller
    {
        public IUser _user = user;

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(SignIn userLogin)
        {
            if (ModelState.IsValid)
            {
                var response = await _user.Login(userLogin.Email, userLogin.Password);
                if (!string.IsNullOrEmpty(response.Error))
                {
                    ViewBag.Error = response.Error.Replace("_", " ");
                    return View();
                }
                HttpContext.Session.Remove("tokenClientResend");
                if (response.IsAuthenticated)
                {
                    HttpContext.Session.SetString("tokenClient", response.TokenID);
                    return RedirectToAction("Index", "Home");
                }
                HttpContext.Session.SetString("tokenClientResend", response.TokenID);
                ViewBag.IsAutenticatedError = "Your access is not authenticated, please confirm in your email.";

            }
            return View(userLogin);
        }



        [HttpGet]
        public IActionResult SignUp() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUp signUp,IFormCollection form)
        {
            
            if (ModelState.IsValid)
            {
                var captchaVerification = await ValidationRecaptcha(form);
                if (captchaVerification.Success)
                {
                    var response = await _user.RegisterAccount(signUp.Email, signUp.Password, signUp.Name);
                    if (!string.IsNullOrEmpty(response.Error))
                        return RedirectToAction("Error");
                    return RedirectToAction("ValidateYourEmail", "User");
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

        [HttpGet]
        public IActionResult ValidateYourEmail() => View();
        [HttpGet]
        public IActionResult ReSendValidateInEmail(string email)
        {
            _user.ReSendVerificationEmail(HttpContext.Session.GetString("tokenClientResend"));
            return RedirectToAction("ValidateYourEmail", "User");
        }

        [HttpGet]
        public IActionResult PasswordReset() => View(new ResetPWD());

        [HttpPost]
        public async Task<IActionResult> PasswordReset(IFormCollection form)
        {
            var captchaVerification = await ValidationRecaptcha(form);

            if (captchaVerification.Success)
            {
                await user.ResetPasswordAccess(form["Email"]);
                return RedirectToAction("PasswordSent");
            }
            else // reCAPTCHA falhou
            {
                var errors = string.Join(", ", captchaVerification.ErrorCodes);
                errors = errors.Replace("invalid-input-response", "Invalid CAPTCHA");
                return View("PasswordReset", new ResetPWD() { Error = errors });
            }
        }

        [HttpGet]
        public IActionResult PasswordSent() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<CaptchaResponse?> ValidationRecaptcha(IFormCollection form)
        {
            var recaptchaResponse = form["g-recaptcha-response"];
            var client = new HttpClient();
            var result = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={"6LedYBoqAAAAAPfQH1WObVJwBsTd1KWq_lWiH1jI"}&response={recaptchaResponse}");

            return JsonConvert.DeserializeObject<CaptchaResponse>(result);

        }
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
