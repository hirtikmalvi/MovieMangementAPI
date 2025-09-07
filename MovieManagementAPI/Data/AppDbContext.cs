using Microsoft.EntityFrameworkCore;
using MovieManagementAPI.Models;

namespace MovieManagementAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(m =>
            {
                m.HasCheckConstraint("CHK_RATING", "[Rating] >= 1 AND [Rating] <= 10");
                m.HasCheckConstraint("CHK_RELEASEYEAR", "[ReleaseYear] >= 1000 AND [ReleaseYear] <= 2025");
            });
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
