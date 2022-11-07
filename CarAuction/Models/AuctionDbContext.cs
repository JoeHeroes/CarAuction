using Microsoft.EntityFrameworkCore;
using CarAuction.Authorization;
using CarAuction.Models;

namespace CarAuction.Entites
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<InfoSell> InfoSells { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .Property(u => u.bodyType)
                .IsRequired();

            modelBuilder.Entity<InfoSell>()
               .Property(u => u.registrationYear)
               .IsRequired();

            modelBuilder.Entity<Bid>()
               .Property(u => u.timeLeft)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.Email)
               .IsRequired();

            modelBuilder.Entity<Role>()
              .Property(u => u.Name)
              .IsRequired();

            modelBuilder.Entity<Vehicle>().ToTable("Vehicle");
            //modelBuilder.Entity<University>().HasData();

        }
    }
}
