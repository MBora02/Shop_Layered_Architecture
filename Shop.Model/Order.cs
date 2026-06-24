using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model
{
    public class Order
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }


        public string Status { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
