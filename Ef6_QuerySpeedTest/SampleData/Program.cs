using SampleData.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SampleData
{
    class Program
    {
        private const string connectionString = "Data Source=lpc:localhost;Initial Catalog=EF_SpeedTest;Integrated Security=True";

        static void Main(string[] args)
        {
            CreateSampleData();

            Console.WriteLine("Console ready. Wait for Save Done!");
            Console.ReadKey();
        }

        private async static void CreateSampleData()
        {
            using (var context = CreateDbContext())
            {
                if (!await context.Database.EnsureCreatedAsync())
                    return;

                var customers = DataGenerator.GenerateData(200);
                await context.Customers.AddRangeAsync(customers);

                var countEntites = CountEntities(customers);
                Console.WriteLine($"{countEntites} Einträge werden gepeichert..");
                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} - Daten Speichern beginnt.");
                var stopwatch = Stopwatch.StartNew();
                await context.SaveChangesAsync();
                stopwatch.Stop();
                var elapsed = stopwatch.Elapsed;
                Console.WriteLine($"Das Speichern benötigte: {elapsed.ToString(@"dd\.hh\:mm\:ss")}");
            }

            AddMoreCustomers(10_000);

            int CountEntities(IEnumerable<Customer> customers)
            {
                var count = customers.Count();
                var orders = customers.SelectMany(c => c.Orders);
                count += orders.Count();
                var orderDetails = orders.SelectMany(o => o.OrderDetails);
                count += orderDetails.Count();
                var products = orderDetails.Select(od => od.Product).Distinct();
                count += products.Count();

                return count;
            }
        }
        private async static void AddMoreCustomers(int count)
        {
            using (var context = CreateDbContext())
            {
                var customerFiller = FillerGenerator.GetCustomerFiller();

                var customers = customerFiller.Create(count);
                await context.AddRangeAsync(customers);
                await context.SaveChangesAsync();
                Console.WriteLine("Save Done");
            }
        }

        private static SampleDbContext CreateDbContext() => new SampleDbContext(connectionString);
    }
}
