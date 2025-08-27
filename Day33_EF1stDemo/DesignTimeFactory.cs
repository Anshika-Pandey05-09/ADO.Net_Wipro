// Adding a design time factory so that dotnet ef finds the DbContext during migration
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Day33_EF1stDemo.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TrainingContext>
    {
        public TrainingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TrainingContext>();
            optionsBuilder.UseSqlServer("Data Source=DELL;Initial Catalog=Day32_BookDB;Integrated Security=True;TrustServerCertificate=True;");

            return new TrainingContext(optionsBuilder.Options);
        }
    }
}