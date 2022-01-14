using PricingEngine.Models;

namespace PricingEngine.Services
{
    public interface IQuotationSystem
    {
        PriceResponse GetPrice(dynamic priceRequest);
    }
}
