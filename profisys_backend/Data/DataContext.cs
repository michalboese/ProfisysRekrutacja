using Microsoft.EntityFrameworkCore;
using profisys_backend.Entities;

namespace profisys_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Documents> Documents { get; set; }
        public DbSet<DocumentItems> DocumentItems { get; set; }
    }
}
