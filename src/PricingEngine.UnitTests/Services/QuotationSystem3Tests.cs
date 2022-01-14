using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PricingEngine.Models;
using PricingEngine.Services;

namespace PricingEngine.UnitTests.Services
{
    [TestClass]
    public class QuotationSystem3Tests
    {
        private IQuotationSystem QuotationSystem { get; set; }

        [TestInitialize]
        public void Setup()
        {
            QuotationSystem = new QuotationSystem3();
        }

        [TestMethod]
        public void When_Request_Is_Valid_Then_Correct_Results_Returned()
        {
            var request = new PriceRequest()
            {
                RiskData = new RiskData()
                {
                    FirstName = "Steve",
                    LastName = "Jones",
                    Make = "iPhone",
                    Value = 200,
                    DOB = DateTime.Parse("1980-01-01")
                }
            };

            var result = QuotationSystem.GetPrice(request);

            Assert.AreEqual(result.IsSuccess, true);
            Assert.AreEqual(result.Price, 92.67M);
        }
    }
}
