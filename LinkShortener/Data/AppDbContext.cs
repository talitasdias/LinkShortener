using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UrlMappings> urlMappings { get; set; }
    }

    public class UrlMappings
    {
        public int Id { get; set; }
        public string ShortenUrl { get; set; }
        public string LongUrl { get; set; }
    }
}
