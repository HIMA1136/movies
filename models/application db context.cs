using Microsoft.EntityFrameworkCore;

namespace api_movia.models
{
    public class application_db_context : DbContext
    { 

       public application_db_context(DbContextOptions<application_db_context> options) : base(options)
          { }
    public DbSet<genera> generas { set; get; }
    public DbSet<movie> movies { set; get; }
    }
}
