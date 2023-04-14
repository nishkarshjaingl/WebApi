using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ConsumeApi.Models
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            modelBuilder.Entity<Product>()
              .HasOne<UserDetails>(s => s.UserDetails)
              .WithMany(ta => ta.Products)
              .HasForeignKey(u => u.UserId)
              .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cart>()
             .HasOne<UserDetails>(s => s.UserDetails)
             .WithMany(ta => ta.Carts)
             .HasForeignKey(u => u.UserId)
             .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
            .HasOne<Category>(p => p.Categories)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.CatId);


        }

        public DbSet<UserDetails> UserDetails { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }

        public DbSet<UserType> UserTypes { get; set; }

    }
}
