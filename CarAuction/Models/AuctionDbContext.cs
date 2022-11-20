using CarAuction.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Models
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .Property(u => u.BodyType)
                .IsRequired();

            modelBuilder.Entity<Sell>()
               .Property(u => u.VIN)
               .IsRequired();

            modelBuilder.Entity<Bid>()
               .Property(u => u.CurrentBid)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.Email)
               .IsRequired();

            modelBuilder.Entity<Role>()
              .Property(u => u.Name)
              .IsRequired();
        }
    }
}
