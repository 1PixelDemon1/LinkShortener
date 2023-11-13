using Microsoft.EntityFrameworkCore;
using ShortWeb.Model.Models;

namespace ShortWeb.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() {}
        public ApplicationDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortLink>().HasData(new ShortLink[]
            {
                new()
                {
                    Id = 1,
                    ShortenedLink = "1111",
                    Link = "https://github.com/1PixelDemon1"
                }
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ShortLink> ShortLinks { get; set; }

    }
}
