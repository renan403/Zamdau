using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zamdau.Controllers
{
    public class TestesController : Controller
    {

        private const string RecaptchaSecretKey = "****";

        [HttpPost]
        public async Task<IActionResult> ProcessarFormulario(string email, string mensagem, string gRecaptchaResponse)
        {
            if (await IsReCaptchaValid(gRecaptchaResponse))
            {
                // Processe o formulário
                return View("Sucesso");
            }
            else
            {
                // Exiba uma mensagem de erro
                ModelState.AddModelError(string.Empty, "Por favor, complete o CAPTCHA corretamente.");
                return View("Erro");
            }
        }

        private async Task<bool> IsReCaptchaValid(string gRecaptchaResponse)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={RecaptchaSecretKey}&response={gRecaptchaResponse}");
            var recaptchaResult = JsonConvert.DeserializeObject<ReCaptchaResponse>(response);
            return recaptchaResult.Success;
        }
    }

    public class ReCaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public string[] ErrorCodes { get; set; }
    }

}
