using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [HttpGet]
        public IActionResult PasswordReset()
        {
            return View(new Models.ResetPWD());
        }
        [HttpPost]
        public IActionResult PasswordReset(Models.SignIn model)
        {
            return View();
        }
        public IActionResult PasswordSent()
        {
            return View();
        }

        private const string RECAPTCHA_SECRET = "6LedYBoqAAAAAPfQH1WObVJwBsTd1KWq_lWiH1jI";

        [HttpPost]
        public async Task<ActionResult> Submit(IFormCollection form)
        {
            var recaptchaResponse = form["g-recaptcha-response"];
            var client = new HttpClient();
            var result = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={RECAPTCHA_SECRET}&response={recaptchaResponse}");

            var captchaVerification = JsonConvert.DeserializeObject<ReCaptchaResponse>(result);

            if (captchaVerification.Success)
            {

                // reCAPTCHA foi validado com sucesso
                return RedirectToAction("PasswordSent");
            }
            else
            {
                // reCAPTCHA falhou
                var errors = string.Join(", ", captchaVerification.ErrorCodes);
                errors = errors.Replace("invalid-input-response", "Invalid CAPTCHA");

                return View("PasswordReset", new Models.ResetPWD() { Error = errors }); 
            }
        }
    }

    public class ReCaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")] 
        public string[] ErrorCodes { get; set; }

        [JsonProperty("challenge_ts")]
        public DateTime challenge_ts { get; set; }
    }
}
