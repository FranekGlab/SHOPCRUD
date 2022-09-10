using Microsoft.EntityFrameworkCore;
using SHOPCRUD.Models.DomainModels;

namespace SHOPCRUD.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Watch> Watches { get; set; }

    }
}
