using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace EfCore_1_1_2_QuerySpeedTest
{
    class Program
    {
        /*
         * Achtung!
         * Beachten Sie, dass Ef Core 1.1.2 noch keine
         * komplexen Properties (in Ef Core genannt: Owned Properties)
         * möglich sind. Somit hat Ef Core beim persistieren eines Customers
         * ein klein weniger Arbeit, da keine neue Instanz von Address 
         * erstellt werden muss.
         * Vergleiche:
         *  - EfCore_1_1_2_QuerySpeedTest.Models.Customer
         *  - EfCore_QuerySpeedTest.Models.Customer
         */

        private const string connectionString = "Data Source=lpc:localhost;Initial Catalog=EF_SpeedTest;Integrated Security=True";

        static void Main(string[] args)
        {
            long totalWith = 0;
            long totalWithout = 0;

            FirstQuery();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
                var (with, without) = SpeedTest();
                totalWith += with;
                totalWithout += without;
            }

            Console.WriteLine("\n\n\nSchnitt von 100 Versuchen:");
            Console.WriteLine($"Benötigte Zeit With Tracking: {totalWith / 100} Millisekunden");
            Console.WriteLine($"Benötigte Zeit WithOut Tracking: {totalWithout / 100} Millisekunden");

            Console.ReadKey();
        }

        private static void FirstQuery()
        {
            using (var context = CreateDbContext())
            {
                var o = context.Orders.First();
            }
        }

        private static (long withoutTracking, long withTracking) SpeedTest()
        {
            long without = 0;
            long with = 0;

            // With Tracking
            using (var context = CreateDbContext())
            {
                Console.WriteLine("\n\n\nSpeedtest with Tracking:");

                var stopwatch = Stopwatch.StartNew();
                var customers = context.Customers.ToList();
                stopwatch.Stop();

                var customerCount = customers.Count();
                Console.WriteLine($"{customerCount} Entitäten wurden gladen.");
                with = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"Benötigte Zeit: {with} Millisekunden");
            }

            // Without Tracking
            using (var context = CreateDbContext())
            {
                Console.WriteLine("\nSpeedtest without Tracking:");

                var stopwatch = Stopwatch.StartNew();
                var customers = context.Customers.AsNoTracking().ToList();
                stopwatch.Stop();

                var customerCount = customers.Count();
                Console.WriteLine($"{customerCount} Entitäten wurden gladen.");
                without = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"Benötigte Zeit: {without} Millisekunden");
            }

            return (with, without);
        }

        private static SampleDbContext CreateDbContext() => new SampleDbContext(connectionString);
    }
}
