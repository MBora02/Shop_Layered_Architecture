using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Data;
using Shop.Model;

namespace Shop.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        public OrderController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var result=_context.Order
                .Include(r=>r.Customer)
                .Include(r=>r.Product).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Musteriler = new SelectList(
                _context.Customers,
                "CustomerId",
                "FirstName"
            );

            ViewBag.Odalar = new SelectList(
                _context.Products,
                "ProductId",
                "ProductName"
            );
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            order.OrderDate = DateTime.Now;
            _context.Order.Add(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = _context.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Musteriler = new SelectList(
                _context.Customers,
                "CustomerId",
                "FirstName"
            );

            ViewBag.Odalar = new SelectList(
                _context.Products,
                "ProductId",
                "ProductName"
            );
            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            _context.Order.Update(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var order = _context.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Musteriler = new SelectList(
                _context.Customers,
                "CustomerId",
                "FirstName"
            );

            ViewBag.Odalar = new SelectList(
                _context.Products,
                "ProductId",
                "ProductName"
            );
            return View(order);
        }

        [HttpPost]
        public IActionResult Delete(Order order)
        {
            _context.Order.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
