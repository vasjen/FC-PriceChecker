using Microsoft.EntityFrameworkCore;
using Cards.Models;

namespace Cards.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<Card> Cards { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config)
        : base(options)
        {
            _config = config;
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = _config.GetConnectionString("ConnectionToDb"); 
            optionsBuilder.UseNpgsql(connection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}