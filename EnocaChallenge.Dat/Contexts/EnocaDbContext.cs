using EnocaChallenge.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnocaChallenge.Data.Contexts
{
    public class EnocaDbContext : DbContext
    {       
        public EnocaDbContext(DbContextOptions<EnocaDbContext> options) : base(options)
        {
        }

        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CarrierReport> CarrierReports { get; set; }
        public DbSet<CarrierConfiguration> CarrierConfigurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Carrier>()
                .Property(c => c.CarrierName)
                .IsRequired();
            
           modelBuilder.Entity<CarrierConfiguration>()
                .Property(c => c.CarrierCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderCarrierCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<CarrierReport>()
               .Property(c => c.CarrierCost)
               .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}