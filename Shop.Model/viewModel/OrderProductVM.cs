using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model.viewModel
{
    public class OrderProductVM
    {
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
