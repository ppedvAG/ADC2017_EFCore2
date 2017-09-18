using System;
using System.Collections.Generic;

namespace SampleData.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public Address Adress { get; set; }

        public DateTime LastUpdated { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Order> Orders { get; } = new HashSet<Order>();
    }
}
