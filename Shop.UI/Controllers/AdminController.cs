using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Data;
using System.Linq;

namespace Shop.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var totalCustomers = _context.Customers.Count();
            var totalProducts = _context.Products.Count();
            var totalOrders = _context.Order.Count();
            var totalSales = _context.Order.Any() 
                ? _context.Order.Sum(o => o.Quantity * o.UnitPrice) 
                : 0;

            ViewBag.TotalCustomers = totalCustomers;
            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalSales = totalSales;

            // Fetch recent orders to display on the dashboard
            var recentOrders = _context.Order
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .Select(o => new
                {
                    o.OrderId,
                    o.OrderDate,
                    CustomerName = _context.Customers
                        .Where(c => c.CustomerId == o.CustomerId)
                        .Select(c => c.FirstName + " " + c.LastName)
                        .FirstOrDefault() ?? "Bilinmeyen",
                    ProductName = _context.Products
                        .Where(p => p.ProductId == o.ProductId)
                        .Select(p => p.ProductName)
                        .FirstOrDefault() ?? "Bilinmeyen",
                    o.Quantity,
                    Total = o.Quantity * o.UnitPrice,
                    o.Status
                })
                .ToList();

            ViewBag.RecentOrders = recentOrders;

            return View();
        }
    }
}
