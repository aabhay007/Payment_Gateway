namespace Payment_Gateway.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public string ChargeId { get; set; }
        public string Email { get; set; }
        public long Amount { get; set; }
    }
}
