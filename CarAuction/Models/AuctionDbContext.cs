using Microsoft.EntityFrameworkCore;

namespace CarAuction.Models
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Bidder> Bidders { get; set; }
        public DbSet<Watch> Watches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .Property(u => u.BodyType)
                .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.Email)
               .IsRequired();

            modelBuilder.Entity<Role>()
              .Property(u => u.Name)
              .IsRequired();


            modelBuilder
              .Entity<Watch>()
              .HasKey(t => t.Id);

            modelBuilder
             .Entity<Watch>()
             .HasOne(c => c.VehicleMany)
             .WithMany(c => c.Users)
             .HasForeignKey(cl => cl.VehiclesId);

            modelBuilder
             .Entity<Watch>()
             .HasOne(c => c.UserMany)
             .WithMany(c => c.Vehicles)
             .HasForeignKey(cl => cl.UserId);


        }
    }
}
