using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MovieShopDbContext>
    {
        public MovieShopDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MovieShopDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost,1434; Database=MovieShop; User Id=sa; Password=Your$tr0ngP@ssw0rd!; Encrypt=True; TrustServerCertificate=True;");
            
            return new MovieShopDbContext(optionsBuilder.Options);
        }
    }
}
