using System;
using System.Collections.Generic;

namespace EfCore_1_1_2_QuerySpeedTest.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }

        public DateTime LastUpdated { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Order> Orders { get; } = new HashSet<Order>();
    }
}
