using Microsoft.EntityFrameworkCore;
using RentCar.Models;

namespace RentCar.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MsCustomer> Customers { get; set; }
        public DbSet<MsCar> Cars { get; set; }
        public DbSet<MsCarImages> CarImages { get; set; }
        public DbSet<TrRental> Rentals { get; set; }
        public DbSet<LtPayment> Payments { get; set; }
        public DbSet<MsEmployee> Employees { get; set; }
        public DbSet<TrMaintenance> Maintenances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MsCar>()
                .Property(c => c.PricePerDay)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TrRental>()
                .Property(r => r.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<LtPayment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TrMaintenance>()
                .Property(m => m.Cost)
                .HasPrecision(18, 2);
        }
    }
}