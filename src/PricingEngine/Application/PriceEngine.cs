using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using PricingEngine.Models;
using PricingEngine.Services;

namespace PricingEngine.Application
{
    public class PriceEngine : IPriceEngine
    {
        private readonly IEnumerable<IQuotationSystem> _quotationSystems;

        public PriceEngine(IEnumerable<IQuotationSystem> quotationSystems)
        {
            _quotationSystems = quotationSystems;
        }

        private Tuple<bool, string> Validate(PriceRequest request)
        {
            if (request.RiskData == null)
            {
                return new Tuple<bool, string>(false, "Risk Data is missing");
            }

            if (string.IsNullOrEmpty(request.RiskData.FirstName))
            {
                return new Tuple<bool, string>(false, "First name is required");
            }

            if (string.IsNullOrEmpty(request.RiskData.LastName))
            {
                return new Tuple<bool, string>(false, "Surname is required");
            }

            if (request.RiskData.Value == 0)
            {
                return new Tuple<bool, string>(false, "Value is required");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        public dynamic GetPrice(PriceRequest request)
        {
            dynamic response = new ExpandoObject();
            var validate = Validate(request);

            if (!validate.Item1)
            {
                response.Error = validate.Item2;
                return response;
            }

            var bestQuote = _quotationSystems
                .Select(quotationSystem => quotationSystem.GetPrice(request))
                .Where(quote => quote.IsSuccess)
                .Aggregate((q1, q2) => q1.Price < q2.Price ? q1 : q2);

            response.Name = bestQuote.Name;
            response.Price = bestQuote.Price;
            response.Tax = bestQuote.Tax;

            try
            {
                response.Error = bestQuote.Error;
            }
            catch 
            {
                response.Error = null;
            }

            return response;
        }
    }
}
