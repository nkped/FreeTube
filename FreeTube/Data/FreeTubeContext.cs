using FreeTube.Models;
using Microsoft.EntityFrameworkCore;

namespace FreeTube.Data
{
    public class FreeTubeContext : DbContext
    {
        public FreeTubeContext(DbContextOptions<FreeTubeContext> options) : base(options)
        {
        }
        public DbSet<FreeTube.Models.Customer> Customers { get; set; }
        public DbSet<FreeTube.Models.Movie> Movies { get; set; }
        public DbSet<FreeTube.Models.MembershipType> MembershipType { get; set; }
        public DbSet<FreeTube.Models.Genre> Genre { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MembershipType>().HasData(
                new MembershipType { Id = 1, Name = "Pay as you go", SignUpFee = 0, DurationInMonths = 0, DiscountRate = 0 },
                new MembershipType { Id = 2, Name = "Monthly", SignUpFee = 30, DurationInMonths = 1, DiscountRate = 10 },
                new MembershipType { Id = 3, Name = "Quarterly", SignUpFee = 90, DurationInMonths = 3, DiscountRate = 15 },
                new MembershipType { Id = 4, Name = "Yearly", SignUpFee = 300, DurationInMonths = 12, DiscountRate = 20 }
                );
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Drama" },
                new Genre { Id = 4, Name = "Romance" },
                new Genre { Id = 5, Name = "Thriller" }
            );
        }
    }
}
