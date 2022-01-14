using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PricingEngine.Models;
using PricingEngine.Services;

namespace PricingEngine.UnitTests.Services
{
    [TestClass]
    public class QuotationSystem1Tests
    {
        private IQuotationSystem QuotationSystem { get; set; }

        [TestInitialize]
        public void Setup()
        {
            QuotationSystem = new QuotationSystem1();
        }

        [TestMethod]
        public void When_DOB_Is_Empty_Then_Error_Is_Returned()
        {
            var request = new PriceRequest()
            {
                RiskData = new RiskData()
                {
                    FirstName = "Steve",
                    LastName = "Jones",
                    Make = "iPhone",
                    Value = 200
                }
            };

            var result = QuotationSystem.GetPrice(request);

            Assert.AreEqual(result.IsSuccess, false);
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
            Assert.AreEqual(result.Price, 123.45M);
        }
    }
}
