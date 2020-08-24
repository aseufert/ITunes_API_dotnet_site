using Microsoft.EntityFrameworkCore;
using ITunes_API_dotnet_site.Models;

namespace ITunes_API_dotnet_site.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TopApp> TopApps { get; set; }
        public DbSet<Genre> AppGenre { get; set; }
        public DbSet<TopAppGenres> TopAppGenres { get; set; }
        public DbSet<Application> MobileApp { get; set; }
        public DbSet<Review> Review { get; set; }
    }
}