using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using MarinaManagementSystem.Models;


namespace MarinaManagementSystem.Data
{
    public class MarinaDbContext : DbContext
    {
        public MarinaDbContext(DbContextOptions<MarinaDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Boat> Boats { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }

}
