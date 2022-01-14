using Microsoft.VisualStudio.TestTools.UnitTesting;
using PricingEngine.Models;
using PricingEngine.Services;

namespace PricingEngine.UnitTests.Services
{
    [TestClass]
    public class QuotationSystem2Tests
    {
        private IQuotationSystem QuotationSystem { get; set; }

        [TestInitialize]
        public void Setup()
        {
            QuotationSystem = new QuotationSystem2();
        }

        [TestMethod]
        public void When_Make_Is_Invalid_Then_Error_Is_Returned()
        {
            var request = new PriceRequest()
            {
                RiskData = new RiskData()
                {
                    FirstName = "Steve",
                    LastName = "Jones",
                    Make = "notvalid",
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
                    Make = "examplemake2",
                    Value = 200
                }
            };

            var result = QuotationSystem.GetPrice(request);

            Assert.AreEqual(result.IsSuccess, true);
            Assert.AreEqual(result.Price, 234.56M);
        }
    }
}
