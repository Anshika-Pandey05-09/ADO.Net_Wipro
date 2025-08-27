// Creating a training context DBContext for mapping entities to the database
using Microsoft.EntityFrameworkCore;
using Day33_EF1stDemo.Models;

namespace Day33_EF1stDemo.Data
{
    public class TrainingContext : DbContext
    {
        public TrainingContext(DbContextOptions<TrainingContext> options) : base(options) // here options is the connection string
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}