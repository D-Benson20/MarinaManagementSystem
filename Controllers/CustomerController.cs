using MarinaManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarinaManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MarinaDbContext _context;

        public CustomerController(MarinaDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard(int customerId)
        {
            var customer = _context.Customers
                .Include(c => c.Boats)
                .Include(c => c.Bookings)
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
    }
}
