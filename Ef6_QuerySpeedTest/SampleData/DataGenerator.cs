using SampleData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleData
{
    public static class DataGenerator
    {
        private static IEnumerable<Product> GenerateProducts(int countProducts)
        {
            var productFiller = FillerGenerator.GetProductFiller();

            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} - Produkte generieren gestartet.");
            var products = productFiller.Create(countProducts);
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} - Produkte generieren beendet.");

            return products;
        }
        public static IEnumerable<Customer> GenerateData(int countCustomers)
        {
            var random = new Random();
            var (customerFiller, orderFiller, orderDetailsFiller) = FillerGenerator.GetFillers();

            var customers = customerFiller.Create(countCustomers);
            Console.WriteLine($"{customers.Count()} Kunden generiert.");

            foreach(var c in customers)
            {
                var ordersPerCustomer = orderFiller.Create(random.Next(0, 20));
                foreach(var o in ordersPerCustomer)
                {
                    c.Orders.Add(o);

                    var orderDetailsPerOrder = orderDetailsFiller.Create(random.Next(1, 10));
                    foreach(var od in orderDetailsPerOrder)
                        o.OrderDetails.Add(od);
                }
            }

            var orders = customers.SelectMany(c => c.Orders);
            Console.WriteLine($"{orders.Count()} Bestellungen generiert.");

            var orderDetails = orders.SelectMany(o => o.OrderDetails);
            Console.WriteLine($"{orderDetails.Count()} BestellDetails generiert.");

            var products = GenerateProducts(countCustomers).ToList();

            foreach(var od in orderDetails)
            {
                var randomProduct = products[random.Next(0, products.Count)];
                od.Product = randomProduct;
                od.Price = od.Quantity * randomProduct.Price;
            }

            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} - Daten generieren beendet.");
            return customers;
        }
    }
}