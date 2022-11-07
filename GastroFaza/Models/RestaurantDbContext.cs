using GastroFaza.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Models
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Tablee> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Address>()
               .Property(a => a.Street)
               .IsRequired()
               .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Dish>()
               .Property(d => d.Name)
               .IsRequired();

            modelBuilder.Entity<User>()
              .Property(a => a.Email)
              .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(a => a.Name)
                .IsRequired();

            modelBuilder.Entity<Restaurant>()
               .Property(r => r.Name)
               .IsRequired()
               .HasMaxLength(25);




            modelBuilder.Entity<Tablee>()
              .Property(r => r.Busy)
              .IsRequired();

            modelBuilder.Entity<Tablee>()
             .Property(r => r.Seats)
             .IsRequired();


            modelBuilder.Entity<Order>()
              .Property(r => r.Price)
              .IsRequired();

        }
    }
}
