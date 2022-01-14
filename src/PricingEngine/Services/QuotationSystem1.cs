using PricingEngine.Models;

namespace PricingEngine.Services
{
    public class QuotationSystem1 :IQuotationSystem
    {
        private const string url = "http://quote-system-1.com";
        private const int port = 1234;

        public PriceResponse GetPrice(dynamic priceRequest)
        {
            if (priceRequest.RiskData.DOB == null)
            {
                return new PriceResponse
                {
                    IsSuccess = false,
                    Error = "DOB is required for Test Name",
                    Price = -1
                };
            }

            return new PriceResponse
            {
                Price = 123.45M,
                IsSuccess = true,
                Name = "Test Name",
                Tax = 123.45M * 0.12M
            };
        }
    }
}
