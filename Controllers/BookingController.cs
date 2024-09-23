using MarinaManagementSystem.Data;
using MarinaManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MarinaManagementSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly MarinaDbContext _context;

        public BookingController(MarinaDbContext context)
        {
            _context = context;
        }

        public IActionResult Create(int customerId)
        {
            var customer = _context.Customers
                .Include(c => c.Boats)
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            // Use ViewBag to pass the boat list to the view
            ViewBag.Boats = new SelectList(customer.Boats, "BoatId", "Name");

            // Pass CustomerId directly in a new Booking model
            return View(new Booking { CustomerId = customerId });
        }

        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            // Fetch the customer from the database using CustomerId
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == booking.CustomerId);
            if (customer == null)
            {
                ModelState.AddModelError(nameof(booking.CustomerId), "Customer does not exist.");
            }

            // Fetch the boat from the database using BoatId
            var boat = _context.Boats.FirstOrDefault(b => b.BoatId == booking.BoatId);
            if (boat == null)
            {
                ModelState.AddModelError(nameof(booking.BoatId), "Boat does not exist.");
            }

            // Ensure valid dates
            if (booking.StartDate >= booking.EndDate)
            {
                ModelState.AddModelError("EndDate", "End Date must be after Start Date.");
            }

            if (ModelState.IsValid)
            {
                booking.Customer = customer;
                booking.Boat = boat;

                _context.Bookings.Add(booking);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Customer", new { customerId = booking.CustomerId });
            }

            // Re-populate the boats list in case of invalid state
            var customerWithBoats = _context.Customers
                .Include(c => c.Boats)
                .FirstOrDefault(c => c.CustomerId == booking.CustomerId);

            if (customerWithBoats != null)
            {
                ViewBag.Boats = new SelectList(customerWithBoats.Boats, "BoatId", "Name");
            }

            return View(booking);
        }

        public IActionResult Edit(int id)
        {
            var booking = _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Boat)
                .FirstOrDefault(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            // Fetch customer and their boats
            var customer = _context.Customers
                .Include(c => c.Boats)
                .FirstOrDefault(c => c.CustomerId == booking.CustomerId);

            ViewBag.Boats = new SelectList(customer?.Boats, "BoatId", "Name", booking.BoatId);

            return View(booking);
        }

        [HttpPost]
        public IActionResult Edit(Booking booking)
        {
            if (booking.StartDate >= booking.EndDate)
            {
                ModelState.AddModelError("EndDate", "End Date must be after Start Date.");
            }

            if (ModelState.IsValid)
            {
                _context.Bookings.Update(booking);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Customer", new { customerId = booking.CustomerId });
            }

            // Fetch customer and their boats in case of an error
            var customerWithBoats = _context.Customers
                .Include(c => c.Boats)
                .FirstOrDefault(c => c.CustomerId == booking.CustomerId);

            if (customerWithBoats != null)
            {
                ViewBag.Boats = new SelectList(customerWithBoats.Boats, "BoatId", "Name", booking.BoatId);
            }

            return View(booking);
        }

        public IActionResult Delete(int id)
        {
            var booking = _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Boat)
                .FirstOrDefault(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            ViewBag.BookingId = booking.BookingId;

            return View(booking);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int bookingid)
        {
            var booking = _context.Bookings.Find(bookingid);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }

            return RedirectToAction("Dashboard", "Customer", new { customerId = booking?.CustomerId });
        }


    }
}
