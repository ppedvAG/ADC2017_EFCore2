using System;
using System.Collections.Generic;

namespace EfCore_1_1_2_QuerySpeedTest.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Freight { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; } = new HashSet<OrderDetail>();
    }
}