using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model
{
    
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Category { get; set; }

        public decimal UnitPrice { get; set; }

        public int Stock { get; set; }

        public int CriticalStockLevel { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
