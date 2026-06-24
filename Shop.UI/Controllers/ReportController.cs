using Microsoft.AspNetCore.Mvc;
using Shop.Data.Data;
using Shop.Model.viewModel;

namespace Shop.UI.Controllers
{
    public class ReportController : Controller
    {
        private readonly AppDbContext _context;
        public ReportController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerOrderReport()
        {
            var result = from o in _context.Order
                         join c in _context.Customers
                         on o.CustomerId equals c.CustomerId
                         select new CustomerOrderVM
                         {
                             CustomerName = c.FirstName + " " + c.LastName,
                             City = c.City,
                             OrderDate = o.OrderDate,
                             UnitPrice = o.UnitPrice,
                             Quantity = o.Quantity
                         };

            return View(result.ToList());
        }

        public IActionResult OrderProductReport()
        {
            var result = from o in _context.Order
                         join p in _context.Products
                         on o.ProductId equals p.ProductId
                         select new OrderProductVM
                         {
                             ProductName = p.ProductName,
                             Category = p.Category,
                             Quantity = o.Quantity,
                             UnitPrice = o.UnitPrice
                         };

            return View(result.ToList());
        }

        public IActionResult CustomerProductReport()
        {
            var result = from o in _context.Order
                         join c in _context.Customers
                         on o.CustomerId equals c.CustomerId
                         join p in _context.Products
                         on o.ProductId equals p.ProductId
                         select new CustomerProductVM
                         {
                             CustomerName = c.FirstName + " " + c.LastName,
                             ProductName = p.ProductName,
                             Quantity = o.Quantity,
                             City = c.City,
                             OrderDate = o.OrderDate
                         };

            return View(result.ToList());
        }

        public IActionResult CustomerOrderCount()
        {
            var result = from o in _context.Order
                         join c in _context.Customers
                         on o.CustomerId equals c.CustomerId
                         group o by c.FirstName + " " + c.LastName into g
                         select new CustomerOrderCountVM
                         {
                             CustomerName = g.Key,
                             OrderCount = g.Count()
                         };

            return View(result.ToList());
        }

        public IActionResult HighStockProducts()
        {
            var result = _context.Products
                .Where(x => x.Stock < x.CriticalStockLevel)
                .OrderBy(x => x.ProductName)
                .ToList();

            return View(result);
        }
    }
}
