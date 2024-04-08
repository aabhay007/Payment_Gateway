using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Payment_Gateway.Data;
using Payment_Gateway.Models;
using Stripe;

namespace Payment_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly StripeSettings _settings;
        private readonly AppDbContext _db;
        public PaymentController(IOptions<StripeSettings> settings,AppDbContext db) 
        {
            _db= db;
            _settings = settings.Value;
            StripeConfiguration.ApiKey = _settings.SecretKey;
        }
       
        [HttpPost("charge")]
        public IActionResult Charge([FromBody] Dictionary<string,string> data)
        {
            if (!data.ContainsKey("token"))
            {
                
                return BadRequest("Token is missing in the request data.");
            }
            var options = new ChargeCreateOptions
            {
                Currency = "usd",
                Amount = Convert.ToInt32(data["amount"]),
                Source = data["token"],
                Description = "Payment successfull!"//sombir
                
            };
            try
            {
            var service = new ChargeService();
            Charge charge = service.Create(options);
                var payment = new PaymentModel()
                {
                    ChargeId = charge.Id,
                    Amount = charge.Amount,
                    Email = charge.BillingDetails.Name,
                };
                _db.PaymentModels.Add(payment);
                _db.SaveChanges();
                return Ok(charge);
            }
            catch 
            {
                return BadRequest("Some Issue occoured 🤕");
            }
        }
}
}
