using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace Payment_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly StripeSettings _settings;
        public PaymentController(IOptions<StripeSettings> settings)
        {
            _settings = settings.Value;
            StripeConfiguration.ApiKey = _settings.SecretKey;
        }
        //[HttpPost("charge")]
        //public IActionResult Charge([FromBody] Dictionary<string, string> data)
        //{
        //    var options = new ChargeCreateOptions
        //    {
        //        Amount = 2000,
        //        Currency = "usd",
        //        Source = data["token"],
        //        Description = "Description"
        //    };

        //    var service = new ChargeService();
        //    var charge = service.Create(options);

        //    return Ok(charge);
        //}
        [HttpPost("charge")]
        public IActionResult Charge([FromBody] Dictionary<string, string> data)
        {
            if (!data.ContainsKey("token"))
            {
                return BadRequest("Token is missing in the request data.");
            }

            var options = new ChargeCreateOptions
            {
                Amount = 2000,
                Currency = "usd",
                Source = data["token"],
                Description = "Description"
            };

            var service = new ChargeService();
            var charge = service.Create(options);

            return Ok(charge);
        }


        [HttpPost("CreatePaymentIntent")]
        public IActionResult CreatePaymentIntent([FromBody] PaymentIntentCreateRequest request)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = request.Amount,
                Currency = request.Currency,
                PaymentMethod = request.PaymentMethod,
                //Amount=4000,
                //Currency="usd",
                //PaymentMethod="pm_card_visa",
                ConfirmationMethod = "manual",
                Confirm = true,
                //ReturnUrl = "http://localhost:7172" 
            };

            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options);

            return Json(new { client_secret = paymentIntent.ClientSecret });
        }


        public class PaymentIntentCreateRequest
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethod { get; set; }
    }


}
}
