using MarinaManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using MarinaManagementSystem.Data;
using System.Data.SqlTypes;

namespace MarinaManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly MarinaDbContext _context;

        public AccountController(MarinaDbContext context)
        {
            _context = context;
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    Postcode = model.Postcode,
                    CreatedAt = DateTime.Now
                };

                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(model);
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login model)
        {
            try
            {
                var customers = _context.Customers.Take(1);

                var customer = _context.Customers
                    .FirstOrDefault(c => c.Email == model.Email && c.Password != null && c.Password == model.Password);

                if (customer != null)
                {
                    return RedirectToAction("Dashboard", "Customer", new { customerId = customer.CustomerId });
                }

                ModelState.AddModelError(string.Empty, "Invalid email or password.");
            }
            catch (SqlNullValueException ex)
            {
                // Log the exception details
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }
            return View(model);
        }


        // GET: Account/Logout
        public IActionResult Logout()
        {
            // Clear session/cookie and redirect to login page
            return RedirectToAction("Login");
        }
    }
}
