namespace PricingEngine.Models
{
    public class PriceResponse
    {
        public decimal Price { get; set; }
        public bool IsSuccess { get; set; }
        public string Name { get; set; }
        public decimal Tax { get; set; }
        public string Error { get; set; }
    }
}
