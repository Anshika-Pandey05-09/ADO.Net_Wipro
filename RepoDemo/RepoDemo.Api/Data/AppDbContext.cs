// This file consists of the application's database context, which manages the entity framework database connections and configurations.
using Microsoft.EntityFrameworkCore;
using RepoDemo.Api.Models;

namespace RepoDemo.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}