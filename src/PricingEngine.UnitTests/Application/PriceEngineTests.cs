using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PricingEngine.Application;
using PricingEngine.Models;
using PricingEngine.Services;

namespace PricingEngine.UnitTests.Application
{
    [TestClass]
    public class PriceEngineTests
    {
        private IPriceEngine PriceEngine { get; set; }

        [TestMethod]
        public void When_Request_Is_Invalid_Then_Error_Is_Returned()
        {
            PriceEngine = new PriceEngine(Mock.Of<List<IQuotationSystem>>());

            var request = new PriceRequest()
            {
                RiskData = new RiskData() //hardcoded here, but would normally be from user input above
                {
                    DOB = DateTime.Parse("1980-01-01"),
                    LastName = "Smith",
                    Make = "Cool New Phone",
                    Value = 500
                }
            };

            var result = PriceEngine.GetPrice(request);

            Assert.AreEqual("First name is required", result.Error);
        }

        [TestMethod]
        public void When_Request_Is_Valid_Best_Price_Is_Returned()
        {
            var q1Result = new PriceResponse();
            q1Result.Price = 200;
            q1Result.IsSuccess = true;
            q1Result.Tax = 12;
            q1Result.Name = "Q1";

            var q2Result = new PriceResponse();
            q2Result.Price = 100;
            q2Result.IsSuccess = true;
            q2Result.Tax = 15;
            q2Result.Name = "Q2";

            var q3Result = new PriceResponse();
            q3Result.Price = 150;
            q3Result.IsSuccess = true;
            q3Result.Tax = 56;
            q3Result.Name = "Q3";

            var q1 = new Mock<IQuotationSystem>();
            var q2 = new Mock<IQuotationSystem>();
            var q3 = new Mock<IQuotationSystem>();

            q1.Setup(q => q.GetPrice(It.IsAny<PriceRequest>())).Returns(q1Result);
            q2.Setup(q => q.GetPrice(It.IsAny<PriceRequest>())).Returns(q2Result);
            q3.Setup(q => q.GetPrice(It.IsAny<PriceRequest>())).Returns(q3Result);

            PriceEngine = new PriceEngine(new List<IQuotationSystem> {q1.Object, q2.Object, q3.Object});

            var request = new PriceRequest()
            {
                RiskData = new RiskData() //hardcoded here, but would normally be from user input above
                {
                    DOB = DateTime.Parse("1980-01-01"),
                    FirstName = "Teddy",
                    LastName = "Smith",
                    Make = "Cool New Phone",
                    Value = 500
                }
            };

            var result = PriceEngine.GetPrice(request);

            Assert.AreEqual(100, result.Price);
            Assert.AreEqual("Q2", result.Name);
        }
    }
}
