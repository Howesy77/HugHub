using PricingEngine.Models;

namespace PricingEngine.Services
{
    public class QuotationSystem3 :IQuotationSystem
    {
        private const string url = "http://quote-system-3.com";
        private const int port = 897;

        public PriceResponse GetPrice(dynamic priceRequest)
        {
            return new PriceResponse
            {
                Price = 92.67M,
                IsSuccess = true,
                Name = "zxcvbnm",
                Tax = 92.67M * 0.12M
            };
        }
    }
}
