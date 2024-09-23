using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarinaManagementSystem.Data;
using MarinaManagementSystem.Models;

namespace MarinaManagementSystem.Controllers
{
    public class BoatController : Controller
    {
        private readonly MarinaDbContext _context;

        public BoatController(MarinaDbContext context)
        {
            _context = context;
        }

        // GET: Boats/Create
        public IActionResult Create(int customerId)
        {
            ViewBag.CustomerId = customerId;
            return View();
        }

        // POST: Boats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Boat boat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boat);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard", "Customer", new { customerId = boat.CustomerId });
            }
            return View(boat);
        }

        // GET: Boats/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var boat = await _context.Boats.FindAsync(id);
            if (boat == null) return NotFound();
            return View(boat);
        }

        // POST: Boats/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Boat boat)
        {
            if (id != boat.BoatId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boat);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Dashboard", "Customer", new { customerId = boat.CustomerId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoatExists(boat.BoatId)) return NotFound();
                    throw;
                }
            }
            return View(boat);
        }

        // GET: Boats/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var boat = await _context.Boats.FindAsync(id);
            if (boat == null) return NotFound();
            return View(boat);
        }

        // POST: Boats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boat = await _context.Boats.FindAsync(id);
            int customerId = boat.CustomerId; // Store customerId before deleting
            _context.Boats.Remove(boat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Dashboard", "Customer", new { customerId });
        }

        private bool BoatExists(int id)
        {
            return _context.Boats.Any(e => e.BoatId == id);
        }
    }
}
