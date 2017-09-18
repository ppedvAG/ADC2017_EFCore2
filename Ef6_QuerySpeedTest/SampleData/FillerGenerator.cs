using SampleData.Models;
using System;
using Tynamix.ObjectFiller;

namespace SampleData
{
    public static class FillerGenerator
    {
        public static
            (Filler<Customer> customerFiller,
             Filler<Order> orderFiller,
             Filler<OrderDetail> orderDetailsFiller) GetFillers() =>(
                GetCustomerFiller(),
                GetOrderFiller(),
                GetOrderDetailsFiller());

        public static Filler<Customer> GetCustomerFiller()
        {
            var random = new Random();

            var addressFiller = new Filler<Address>();
            var addressSetup = addressFiller.Setup()
                .OnProperty(a => a.Country).Use(new CountryName())
                .OnProperty(a => a.City).Use(new CityName())
                .OnProperty(a => a.Street).Use(new StreetName())
                .OnProperty(a => a.ZipCode).Use(() => $"{random.Next(10000, 99999)}")
                .Result;

            var customerFiller = new Filler<Customer>();
            customerFiller.Setup()
                .OnProperty(c => c.Id).IgnoreIt()
                .OnProperty(c => c.Orders).IgnoreIt()
                .OnProperty(c => c.Adress).Use(addressSetup)
                .OnProperty(c => c.Firstname).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(c => c.Lastname).Use(new RealNames(NameStyle.LastName))
                .OnProperty(c => c.Birthdate).Use(new DateTimeRange(DateTime.Now.AddYears(-70), DateTime.Now.AddYears(-15)));

            return customerFiller;
        }

        public static Filler<Order> GetOrderFiller()
        {
            var orderFiller = new Filler<Order>();
            orderFiller.Setup()
                .OnProperty(o => o.Id).IgnoreIt()
                .OnProperty(o => o.Customer).IgnoreIt()
                .OnProperty(o => o.CustomerId).IgnoreIt()
                .OnProperty(o => o.OrderDetails).IgnoreIt()
                .OnProperty(o => o.Freight).Use(new DoubleRange(5, 200))
                .OnProperty(o => o.OrderDate).Use(new DateTimeRange(DateTime.Now.AddYears(-13), DateTime.Now));
            return orderFiller;
        }

        public static Filler<OrderDetail> GetOrderDetailsFiller()
        {
            var orderDetailsFiller = new Filler<OrderDetail>();
            orderDetailsFiller.Setup()
                .OnProperty(o => o.Id).IgnoreIt()
                .OnProperty(o => o.Order).IgnoreIt()
                .OnProperty(o => o.OrderId).IgnoreIt()
                .OnProperty(o => o.Product).IgnoreIt()
                .OnProperty(o => o.ProductId).IgnoreIt()
                .OnProperty(o => o.Price).IgnoreIt()
                .OnProperty(o => o.Quantity).Use(new IntRange(1, 100));
            return orderDetailsFiller;
        }

        public static Filler<Product> GetProductFiller()
        {
            var productFiller = new Filler<Product>();
            productFiller.Setup()
                .OnProperty(p => p.Id).IgnoreIt()
                .OnProperty(p => p.OrderDetails).IgnoreIt()
                .OnProperty(p => p.Name).Use(new PatternGenerator("{A:3}{C:1000}{A:2}"))
                .OnProperty(p => p.Price).Use(new DoubleRange(20, 9999));

            return productFiller;
        }
    }
}