using System.Collections.Generic;

namespace EfCore_1_1_2_QuerySpeedTest.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; } = new HashSet<OrderDetail>();
    }
}