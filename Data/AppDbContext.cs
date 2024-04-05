using Microsoft.EntityFrameworkCore;
using Payment_Gateway.Models;

namespace Payment_Gateway.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
    }
}
