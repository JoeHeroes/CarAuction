using CarAuction.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Models
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        {
        }

        public DbSet<InfoVehicle> Vehicles { get; set; }
        public DbSet<InfoSell> Sells { get; set; }
        public DbSet<InfoBid> Bids { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InfoVehicle>()
                .Property(u => u.BodyType)
                .IsRequired();

            modelBuilder.Entity<InfoSell>()
               .Property(u => u.VIN)
               .IsRequired();

            modelBuilder.Entity<InfoBid>()
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
