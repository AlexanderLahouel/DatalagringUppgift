

using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{

    
        public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
        {
        public DbSet<CustomerEntity> Customer {  get; set; }
        }
    

