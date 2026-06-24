using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model.viewModel
{
    public class CustomerProductVM
    {
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string City { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
