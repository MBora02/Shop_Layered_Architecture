using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model.viewModel
{
    public class CustomerOrderCountVM
    {
        public string CustomerName { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
