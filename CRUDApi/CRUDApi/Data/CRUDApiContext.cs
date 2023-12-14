
using Microsoft.EntityFrameworkCore;
using CRUDApi.Model;

namespace CRUDApi.Data
{
    public class CRUDApiContext : DbContext
    {
        public CRUDApiContext(DbContextOptions<CRUDApiContext> options)
            : base(options)

        { }
            public DbSet<Employee> Employee { get; set; }
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Department> Departments { get; set; }
    }
    
}
