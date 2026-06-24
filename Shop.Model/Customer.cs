using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public int Age { get; set; }

        public string Phone { get; set; }

        public DateTime RegisterDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
