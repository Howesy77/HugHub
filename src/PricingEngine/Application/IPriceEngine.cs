using PricingEngine.Models;

namespace PricingEngine.Application
{
    public interface IPriceEngine
    {
        dynamic GetPrice(PriceRequest request);
    }
}
