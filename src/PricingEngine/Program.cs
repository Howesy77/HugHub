using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PricingEngine.Application;
using PricingEngine.Extensions;
using PricingEngine.Models;
using PricingEngine.Services;

namespace PricingEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Dummy request data
            var request = new PriceRequest()
            {
                RiskData = new RiskData() //hardcoded here, but would normally be from user input above
                {
                    DOB = DateTime.Parse("1980-01-01"),
                    FirstName = "John",
                    LastName = "Smith",
                    Make = "Cool New Phone",
                    Value = 500
                }
            };

            using (var serviceScope = host.Services.CreateScope())
            {
                var provider = serviceScope.ServiceProvider;
                var priceEngine = provider.GetRequiredService<IPriceEngine>();

                var quote = priceEngine.GetPrice(request);

                Console.WriteLine(quote.Error != null
                    ? string.Format("There was an error - {0}", quote.Error)
                    : string.Format("You price is {0}, from insurer: {1}. This includes tax of {2}", quote.Price, quote.Name, quote.Tax));

                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddTransient<IPriceEngine, PriceEngine>();
                    services.RegisterAllTypes<IQuotationSystem>(new[] {typeof(Program).Assembly}, ServiceLifetime.Transient);
                });
        }
    }
}
