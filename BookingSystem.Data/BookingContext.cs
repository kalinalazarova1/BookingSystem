using BookingSystem.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookingSystem.Data
{
    public class BookingContext : IdentityDbContext<AppUser>
    {
        public BookingContext(DbContextOptions<BookingContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Price> Prices { get; set; }

        public DbSet<Site> Sites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                        .SelectMany(t => t.GetProperties())
                        .Where(p => p.ClrType == typeof(decimal)))
                        {
                            property.Relational().ColumnType = "decimal(18, 2)";
                        }

            base.OnModelCreating(modelBuilder);
        }
    }
}
