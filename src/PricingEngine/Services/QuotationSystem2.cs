using PricingEngine.Models;

namespace PricingEngine.Services
{
    public class QuotationSystem2 :IQuotationSystem
    {
        private const string url = "http://quote-system-2.com";
        private const int port = 456;

        public PriceResponse GetPrice(dynamic priceRequest)
        {
            if (priceRequest.RiskData.Make == "examplemake1" ||
                priceRequest.RiskData.Make == "examplemake2" ||
                priceRequest.RiskData.Make == "examplemake3")
            {
                return new PriceResponse
                {
                    Price = 234.56M,
                    Name = "qewtrywrh",
                    IsSuccess = true,
                    Tax = 234.56M * 0.12M
                };
            }

            return new PriceResponse
            {
                IsSuccess = false,
                Error = "Make not valid for qewtrywrh",
                Price = -1
            };
        }
    }
}
